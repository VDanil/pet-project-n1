import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'FITNESS';

  constructor(private router: Router) { }

  public routeHome(): void {
    this.router.navigateByUrl('/home');
  }
}
