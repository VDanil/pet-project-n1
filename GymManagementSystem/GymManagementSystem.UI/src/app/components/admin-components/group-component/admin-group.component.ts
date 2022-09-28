import { Component, OnInit, ɵsetAllowDuplicateNgModuleIdsForTest } from '@angular/core';
import { AdminGroupService } from 'src/app/services/admin-services/admin-group.service';
import { Group } from 'src/app/entities/group';
import { Activity } from 'src/app/entities/activity';
import { group } from '@angular/animations';

@Component({
  selector: 'admin-group-component',
  templateUrl: './admin-group.component.html',
  styleUrls: ['./admin-group.component.css']
})
export class AdminGroupComponent implements OnInit {

  public groups: Group[] = [];

  constructor(public adminGroupService: AdminGroupService) { }

  public ngOnInit() {
    this.adminGroupService.getGroups().subscribe((groups) => { this.groups = groups });
  }

  // Delete group logic
  public deleteGroup(groupId: number): void {
    this.adminGroupService.deleteGroup(groupId).subscribe(() => {
      this.adminGroupService.getGroups().subscribe((groups) => { this.groups = groups });
    });
  }

  // Edit/Add group logic
  public showForm: boolean = false;
  public tempGroup: Group = { GroupId: 0, Description: '', GroupName: '', Activities: [] };
  public tempDayState: String[] = ['No', 'No', 'No', 'No', 'No', 'No', 'No'];
  public tempActivityTime: number = 0;
  public modeOfUpdate: string = '';
  public mistakeText: string = '';

  public showUpdateForm(group?: Group): void {
    this.showForm = true;
    this.resetTempForm();
    // initialize form states for edit(true) or add(false)
    if (group !== undefined) {
      this.adminGroupService.getGroupTimetable(group.GroupId).subscribe((activities) => {
        this.tempGroup = group;
        this.tempGroup.Activities = [];
        for (let activity of activities) {
          this.tempGroup.Activities.push(activity);
        }

        this.tempDayState = ['No', 'No', 'No', 'No', 'No', 'No', 'No'];
        if (this.tempGroup.Activities !== [] && this.tempGroup.Activities !== undefined) {
          for (let i = 0; i < this.tempDayState.length; i++) {
            if ((this.tempGroup.Activities.filter((activity) => activity.WeekdayId === i)).length > 0)
              this.tempDayState[i] = 'Yes';
          }
        }

        let time = activities.pop()?.StartTime;
        if (time !== undefined) this.tempActivityTime = Number(time.charAt(0) + time.charAt(1));
        this.modeOfUpdate = 'edit';
      });
    } else {
      let groupId = 0;
      let element = (this.groups.sort((a, b) => a.GroupId - b.GroupId))[this.groups.length - 1];
      if (element !== undefined) groupId = element.GroupId + 1;
      this.tempGroup = { GroupId: groupId, Description: '', GroupName: '', Activities: [] };
      this.tempDayState = ['No', 'No', 'No', 'No', 'No', 'No', 'No'];
      this.modeOfUpdate = 'add';
    }
  }

  public closeUpdateForm(): void {

    if ((this.tempActivityTime < 0) || (this.tempActivityTime > 22)) {
      this.tempActivityTime = 0;
      this.mistakeText = '0 < Time < 23 ';
      return;
    }

    this.tempGroup.Activities = [];
    for (var i = 0; i < this.tempDayState.length; i++) {
      if (this.tempDayState[i] === 'Yes') {
        this.tempGroup.Activities.push({
          ActivityId: i,
          StartTime: this.convertNumberToTimeStr(this.tempActivityTime),
          EndTime: this.convertNumberToTimeStr(this.tempActivityTime + 1),
          WeekdayId: i,
          GroupId: this.tempGroup.GroupId
        });
      }
    }

    if ((this.tempGroup.GroupName !== '')) {
      if (this.modeOfUpdate === 'add') this.addGroup();
      if (this.modeOfUpdate === 'edit') this.editGroup();
    }

    this.adminGroupService.getGroups().subscribe((groups) => this.groups = groups);
    this.showForm = false;
  }

  private resetTempForm(): void {
    this.tempGroup = { GroupId: 0, Description: 'description', GroupName: 'group name', Activities: [] };
    this.tempDayState = ['No', 'No', 'No', 'No', 'No', 'No', 'No'];
    this.tempActivityTime = 0;
    this.modeOfUpdate = '';
    this.mistakeText = '';
  }

  private convertNumberToTimeStr(timeNum: number): string {
    var timeStr = '';
    var timeFromNum = String(timeNum);

    if (timeFromNum.length === 2) timeStr = timeFromNum + ':00:00';
    if (timeFromNum.length === 1) timeStr = '0' + timeFromNum + ':00:00';

    return timeStr;
  }

  public editGroup(): void {
    this.adminGroupService.editGroup(this.tempGroup)
      .subscribe((group) => {
        if (this.tempGroup.Activities !== undefined && this.tempGroup.Activities.length > 0)
          this.adminGroupService.setGroupTimetable(group.GroupId, this.tempGroup.Activities).subscribe(() =>
            this.adminGroupService.getGroups()
              .subscribe((groups) => this.groups = groups));
        else
          this.adminGroupService.getGroups()
            .subscribe((groups) => this.groups = groups);
      })
  }

  public addGroup(): void {

    console.log(this.tempGroup);

    this.adminGroupService.addGroup(this.tempGroup)
      .subscribe((group) => {
        if (this.tempGroup.Activities !== undefined && this.tempGroup.Activities.length > 0)
          this.adminGroupService.setGroupTimetable(group.GroupId, this.tempGroup.Activities).subscribe(() =>
            this.adminGroupService.getGroups()
              .subscribe((groups) => this.groups = groups));
        else
          this.adminGroupService.getGroups()
            .subscribe((groups) => this.groups = groups);
      })
  }

  public timetableStateChanged(dayId: number): void {
    if (this.tempDayState[dayId] === 'No') {
      this.tempDayState[dayId] = 'Yes';
    } else {
      this.tempDayState[dayId] = 'No';
    }
  }
}
