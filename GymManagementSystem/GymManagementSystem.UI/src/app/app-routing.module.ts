import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VisitorGroupComponent } from './components/visitor-components/visitor-group-component/visitor-group.component';
import { VisitorCoachComponent } from './components/visitor-components/visitor-coach-component/visitor-coach.component';
import { VisitorSubscriptionComponent } from './components/visitor-components/visitor-subscription-component/visitor-subscription.component';
import { VisitorComponent } from './components/visitor-components/visitor-component/visitor.component';
import { AdminGroupComponent } from './components/admin-components/group-component/admin-group.component';


const routes: Routes = [
  { path: 'groups', component: VisitorGroupComponent },
  { path: 'coaches', component: VisitorCoachComponent },
  { path: 'subscription', component: VisitorSubscriptionComponent },
  { path: 'home', component: VisitorComponent },
  { path: 'admin/group', component: AdminGroupComponent },
  // { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', component: VisitorComponent }
]

@NgModule(
  {
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
export class AppRoutingModule { }
