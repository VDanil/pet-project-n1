import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WebApiExecuterService } from '../web-api-executer.service';
import { Group } from 'src/app/entities/group';

@Injectable({
  providedIn: 'root'
})
export class VisitorGroupService implements OnInit {

  public url: string = 'https://localhost:7229/api/visitor';

  constructor(private webClient: WebApiExecuterService) { }

  ngOnInit(): void { }

  public getGroups(): Observable<Group[]> {
    return this.webClient.getData<Group[]>(this.url + '/groups');
  }
}
