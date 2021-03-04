import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './_services/authentication.service';
import { User } from './_models/user';

import './_content/app.less';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent{
  title = 'expensemanager-frontend';
  currentUser: User;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }
}
// The app component is the root component of the application, it defines the root tag
// of the app as <app></app> with the selector property of the @Component decorator.

// It subscribes to the currentUser observable in the authentication service so it can
// reactively show/hide the main navigation bar when the user logs in/out of the application.
// I didn't worry about unsubscribing from the observable here because it's the root component of
// the application, the only time the component will be destroyed is when the application is
// closed which would destroy any subscriptions as well.
