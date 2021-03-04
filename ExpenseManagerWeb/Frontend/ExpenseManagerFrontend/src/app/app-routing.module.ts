import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';

import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {ForgotPasswordComponent} from './forgot-password/forgot-password.component';
import {ExpensesComponent} from './expenses/expenses.component';
import {TransactionComponent} from './transaction/transaction.component';
import {FriendsComponent} from './friends/friends.component';
import {AddFriendComponent} from './add-friend/add-friend.component';
import {AccountComponent} from './account/account.component';
import {ActivityComponent} from './activity/activity.component';

const routes: Routes = [
  {path: '', component: ExpensesComponent, canActivate: [AuthGuard] },
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'forgot-password', component: ForgotPasswordComponent},
  {path: 'expenses', component: ExpensesComponent, canActivate: [AuthGuard] },
  {path: 'transaction', component: TransactionComponent, canActivate: [AuthGuard] },
  {path: 'friends', component: FriendsComponent, canActivate: [AuthGuard] },
  {path: 'add-friend', component: AddFriendComponent, canActivate: [AuthGuard] },
  {path: 'account', component: AccountComponent, canActivate: [AuthGuard] },
  {path: 'activity', component: ActivityComponent, canActivate: [AuthGuard] },

  // otherwise, redirect to home
  {path: '***', redirectTo: '' }
];

export const routing = RouterModule.forRoot(routes);

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }

// Routing for the Angular app is configured as an array of Routes, each component is
// mapped to a path so the Angular Router knows which component to display based on the URL
// in the browser address bar. The home route is secured by passing the AuthGuard to the canActivate
// property of the route.

// The Routes array is passed to the RouterModule.forRoot() method which creates a routing module
// with all of the app routes configured, and also includes all of the Angular Router providers
// and directives such as the <router-outlet></router-outlet> directive. For more information
// on Angular Routing and Navigation see https://angular.io/guide/router.

