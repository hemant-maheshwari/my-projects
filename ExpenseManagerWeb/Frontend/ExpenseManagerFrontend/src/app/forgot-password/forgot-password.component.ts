import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../_services/user.service.client';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AlertService } from '../_services/alert.service';
import { AuthenticationService } from '../_services/authentication.service';
import { MustMatch } from '../_helpers/must-match.validator';
import {User} from '../_models/user';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordForm: FormGroup;
  changePasswordForm: FormGroup;

  loading = false;
  submitted = false;
  user: User;

  constructor(
    private router: Router,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private authenticationService: AuthenticationService
  ) {
    if (this.authenticationService.currentUserValue){
      this.router.navigate(['/']);
    }
  }

  ngOnInit(): void {
    this.forgotPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    });

    this.changePasswordForm = this.formBuilder.group({
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, {
      validators: MustMatch('newPassword', 'confirmPassword')
    });
  }

  get f(){ return this.forgotPasswordForm.controls; }

  get g(){ return this.changePasswordForm.controls; }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.forgotPasswordForm.invalid){
      return;
    }

    this.loading = true;

    this.userService.checkEmailExistence(this.f.email.value)
      .pipe(first())
      .subscribe(
        data => {
          this.user = data;
          if (!this.user){
            this.alertService.error('There is no account associated with this email.');
            this.loading = false;
            this.submitted = false;
            return;
          }
          this.alertService.success('Email Found! Change your password.', true);
          this.loading = false;
          this.submitted = false;
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

  changePassword() {
    this.submitted = true;
    this.alertService.clear();
    if (this.changePasswordForm.invalid){
      return;
    }
    this.user.password = this.g.newPassword.value;
    this.loading = true;
    this.userService.update(this.user)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Password changed successfully.', true);
          this.router.navigate(['/login']);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

}
