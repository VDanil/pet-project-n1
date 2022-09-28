import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { WebApiExecuterService } from 'src/app/services/web-api-executer.service';
import { CredentialsService } from '../credentials.service';

@Component({
  selector: 'auth-component',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthCompoment implements OnInit {

  public isAuthenticated: boolean = false;
  public accessToken: string = '';

  constructor(public webApiExecuterService: WebApiExecuterService,
    public oidcSecurityService: OidcSecurityService,
    public credentialsService: CredentialsService,
    public router: Router) {
  }

  ngOnInit(): void {
    this.oidcSecurityService.checkAuth().subscribe((loginResponse) => {
      this.isAuthenticated = loginResponse.isAuthenticated;
      this.accessToken = loginResponse.accessToken;
      if (loginResponse.isAuthenticated) {
        this.webApiExecuterService.addHeader('Authorization', 'Bearer ' + loginResponse.accessToken);
        this.credentialsService.setCredentials(loginResponse);
      } else {
        this.webApiExecuterService.removeHeader('Authorization');
        this.credentialsService.resetCredentials();
      }
    });
  }

  public login(): void {
    this.oidcSecurityService.authorize();
  }

  public logout(): void {
    this.oidcSecurityService.logoff();
    this.credentialsService.resetCredentials();
    this.isAuthenticated = false;
    this.accessToken = '';
    this.router.navigateByUrl("/home");
  }

  public goToAdminGroup() {
    this.router.navigateByUrl('/admin/group');
  }
}
