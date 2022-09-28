import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'visitor-component',
  templateUrl: './visitor.component.html',
  styleUrls: ['./visitor.component.css']
})
export class VisitorComponent implements OnInit {

  constructor(private router: Router) { }

  public ngOnInit() { }

  public navigateToGroup() {
    this.router.navigateByUrl('/groups');
  }

  public navigateToCoach() {
    this.router.navigateByUrl('/coaches');
  }

  public navigateToSubscription() {
    this.router.navigateByUrl('/subscription');
  }
}
