import { Injectable, OnInit } from '@angular/core';
import { LoginResponse } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: 'root'
})
export class CredentialsService implements OnInit {

  public isAuthenticated: boolean = false;
  public userData: any = [];
  public idToken: string = '';
  public configId: string = 'StandardConfiguration';
  public errorMessage?: string = '';
  public accessToken: string = '';

  constructor() { }

  public ngOnInit(): void { }

  public getCredentials(): LoginResponse {
    const credentials: LoginResponse = {
      isAuthenticated: this.isAuthenticated,
      userData: this.userData,
      idToken: this.idToken,
      configId: this.configId,
      accessToken: this.accessToken
    }
    return credentials;
  }

  public setCredentials(loginResponse: LoginResponse) {
    this.isAuthenticated = loginResponse.isAuthenticated;
    this.userData = loginResponse.userData;
    this.idToken = loginResponse.idToken;
    this.configId = loginResponse.configId;
    this.accessToken = loginResponse.accessToken;
  }

  public resetCredentials() {
    this.isAuthenticated = false;
    this.userData = [];
    this.idToken = '';
    this.configId = 'StandardConfiguration';
    this.errorMessage = '';
    this.accessToken = '';
  }
}
