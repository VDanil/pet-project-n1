import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WebApiExecuterService } from '../web-api-executer.service';
import { Coach } from 'src/app/entities/coach';

@Injectable({
  providedIn: 'root'
})
export class VisitorCoachService implements OnInit {

  public url: string = 'https://localhost:7229/api/visitor';

  constructor(private webClient: WebApiExecuterService) { }

  ngOnInit(): void { }

  public getCoaches(): Observable<Coach[]> {
    return this.webClient.getData<Coach[]>(this.url + '/coaches');
  }
}
