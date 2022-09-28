import { Component, OnInit } from '@angular/core';
import { VisitorGroupService } from 'src/app/services/visitor-services/visitor-group.service';
import { Group } from 'src/app/entities/group';

@Component({
  selector: 'visitor-group-component',
  templateUrl: './visitor-group.component.html',
  styleUrls: ['./visitor-group.component.css']
})
export class VisitorGroupComponent implements OnInit {

  public groups: Group[] = [];

  constructor(public groupService: VisitorGroupService) { }

  public ngOnInit() {
    this.groupService.getGroups().subscribe((groups) => {
      console.log(groups);
      this.groups = groups
    });
  }
}
