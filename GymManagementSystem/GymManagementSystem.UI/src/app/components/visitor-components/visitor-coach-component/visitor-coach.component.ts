import { Component, OnInit } from '@angular/core';
import { VisitorCoachService } from 'src/app/services/visitor-services/visitor-coach.service';
import { Coach } from 'src/app/entities/coach';

@Component({
  selector: 'visitor-coach-component',
  templateUrl: './visitor-coach.component.html',
  styleUrls: ['./visitor-coach.component.css']
})
export class VisitorCoachComponent implements OnInit {

  public coaches: Coach[] = [];

  constructor(public visitorCoachService: VisitorCoachService) { }

  public ngOnInit() {
    this.visitorCoachService.getCoaches().subscribe((coaches) => { this.coaches = coaches });
  }
}
