import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WebApiExecuterService } from '../web-api-executer.service';
import { Subscription } from 'src/app/entities/subscription';

@Injectable({
  providedIn: 'root'
})
export class VisitorSubscriptionService implements OnInit {

  public url: string = 'https://localhost:7229/api/visitor';

  constructor(private webClient: WebApiExecuterService) { }

  ngOnInit(): void { }

  public getSubscriptionPrice(subscription: Subscription): Observable<Subscription> {
    return this.webClient.putData<Subscription>(this.url + '/SubscriptionPrice', subscription);
  }
}
