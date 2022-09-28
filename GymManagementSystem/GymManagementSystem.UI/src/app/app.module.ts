import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { FormsModule } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MessagesComponent } from './components/messages-component/messages.component';
import { AuthCompoment } from './auth/auth-component/auth.component';
import { VisitorComponent } from './components/visitor-components/visitor-component/visitor.component';
import { VisitorGroupComponent } from './components/visitor-components/visitor-group-component/visitor-group.component';
import { VisitorCoachComponent } from './components/visitor-components/visitor-coach-component/visitor-coach.component';
import { VisitorSubscriptionComponent } from './components/visitor-components/visitor-subscription-component/visitor-subscription.component';
import { AdminGroupComponent } from './components/admin-components/group-component/admin-group.component';

@NgModule({
  declarations: [
    AppComponent,
    MessagesComponent,
    AuthCompoment,
    VisitorComponent,
    VisitorGroupComponent,
    VisitorCoachComponent,
    VisitorSubscriptionComponent,
    AdminGroupComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    AuthModule.forRoot({
      config:
      {
        configId: "StandardConfiguration",
        authority: 'https://localhost:5001',
        redirectUrl: 'https://localhost:4200',
        postLogoutRedirectUri: 'https://localhost:4200',
        clientId: "angular.spa.ui",
        scope: "openid profile administrator",
        responseType: 'code',
        // silentRenew: true,
        // useRefreshToken: true,
        logLevel: LogLevel.Debug
      }})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
