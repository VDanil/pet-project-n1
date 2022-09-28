import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WebApiExecuterService } from '../web-api-executer.service';
import { Group } from 'src/app/entities/group';
import { Activity } from 'src/app/entities/activity';

@Injectable({
  providedIn: 'root'
})
export class AdminGroupService implements OnInit {

  public url: string = 'https://localhost:7229/api/group';

  constructor(private webClient: WebApiExecuterService) { }

  ngOnInit(): void { }

  public addGroup(group: Group): Observable<Group> {
    return this.webClient.postData<Group>(this.url, group);
  }

  public getGroup(groupId: number): Observable<Group> {
    return this.webClient.getData<Group>(this.url + '/' + groupId);
  }

  public getGroups(): Observable<Group[]> {
    return this.webClient.getData<Group[]>(this.url);
  }

  public editGroup(group: Group): Observable<Group> {
    return this.webClient.putData(this.url, group);
  }

  public deleteGroup(groupId: number): Observable<any> {
    return this.webClient.deleteData(this.url + '/' + groupId);
  }

  public getGroupTimetable(groupId: number): Observable<Activity[]> {
    return this.webClient.getData<Activity[]>(this.url + '/' + groupId + '/timetable');
  }

  public setGroupTimetable(groupId: number, activities: Activity[]): Observable<Activity[]> {
    return this.webClient.putData<Activity[]>(this.url + '/' + groupId + '/timetable', activities);
  }


}
