import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Subscription } from 'src/app/entities/subscription';
import { VisitorSubscriptionService } from 'src/app/services/visitor-services/visitor-subscription.service';

@Component({
  selector: 'visitor-subscription',
  templateUrl: './visitor-subscription.component.html',
  styleUrls: ['./visitor-subscription.component.css']
})
export class VisitorSubscriptionComponent implements OnInit {

  public subscriptionPrice: string = '';
  public visitingAmount: number = 1;
  public durationInDays: number = 1;

  constructor(public subscriptionService: VisitorSubscriptionService) { }

  public ngOnInit() { }

  public getSubscriptionPrice(): void {

    if (this.visitingAmount > this.durationInDays) {
      this.subscriptionPrice = 'This is too many visits for the specified period';
      return;
    }

    var subscription: Subscription = {
      SubscriptionId: 0,
      BuyDate: new Date().toISOString(),
      StartDate: (new Date().toISOString()).replace('2022', '2023'),

      DurationInDays: Math.ceil(this.durationInDays),
      VisitingAmount: Math.ceil(this.visitingAmount),

      IsFreezed: false,
      FreezeDate: null,
      FreezeDaysAmount: 0,
      Price: 0,
      Activities: null,
      Visits: null
    };

    this.subscriptionService.getSubscriptionPrice(subscription)
      .subscribe((subscription) => {
        this.subscriptionPrice = 'Price: $' + String(subscription.Price);
      });
  }
}
