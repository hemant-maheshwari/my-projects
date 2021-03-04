import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';

import { MustMatch } from '../_helpers/must-match.validator';

import { AuthenticationService } from '../_services/authentication.service';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service.client';
import { AlertService } from '../_services/alert.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})

export class AccountComponent implements OnInit, OnDestroy {
  accountForm: FormGroup;
  loading = false;
  submitted = false;

  currentUser: User;
  currentUserSubscription: Subscription;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private alertService: AlertService
  ) {
    this.currentUserSubscription = this.authenticationService.currentUser.subscribe( x => this.currentUser = x);
  }

  ngOnInit(): void {
    this.accountForm = this.formBuilder.group({
      id: [this.authenticationService.currentUserValue.id],
      firstName: [this.authenticationService.currentUserValue.firstName, Validators.required],
      lastName: [this.authenticationService.currentUserValue.lastName, Validators.required],
      email: [this.authenticationService.currentUserValue.email, [Validators.required, Validators.email]],
      phone: [this.authenticationService.currentUserValue.phone, Validators.required],
      username: [this.authenticationService.currentUserValue.username],
      password: [this.authenticationService.currentUserValue.password, Validators.required],
      confirmPassword: ['', Validators.required]
    }, {
      validators : MustMatch('password', 'confirmPassword')
    });
  }

  get f(){ return this.accountForm.controls; }

  ngOnDestroy() {
    // unsubscribe to ensure no memory leaks
    this.currentUserSubscription.unsubscribe();
  }

  logout(){
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }

  onSubmit(){
    this.submitted = true;

    this.alertService.clear();

    if (this.accountForm.invalid) {
      return;
    }

    this.loading = true;
    this.userService.update(this.accountForm.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['/login']);
          this.logout();
          this.alertService.success('Update Successful. Sign in again.', true);
        },
        error => {
          this.alertService.error('Something went wrong when creating your account. :(');
          this.loading = false;
        });
  }

}
