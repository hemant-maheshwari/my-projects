import { Component, OnInit } from '@angular/core';
import {Friend} from '../_models/friend';
import {Router} from '@angular/router';
import {FormBuilder} from '@angular/forms';
import {AlertService} from '../_services/alert.service';
import {AuthenticationService} from '../_services/authentication.service';
import {FriendsService} from '../_services/friends.service';
import {first} from 'rxjs/operators';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})

export class FriendsComponent implements OnInit {
  submitted: boolean;
  apiResponse: any;
  loading: boolean;
  loadingFriends: boolean;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private friendService: FriendsService
  ) {
    this.apiResponse = [];
    this.loading = false;
    this.loadingFriends = true;
  }

  ngOnInit(): void {
      this.friendService.getAllFriendsByUserId(this.authenticationService.currentUserValue.id)
        .pipe(first())
        .subscribe(
          data => {
            this.apiResponse = data;
            this.loadingFriends = false;
          },
          error => {
            this.alertService.error(error);
            this.loadingFriends = false;
          }
        );
  }

  getAllFriendsByUserId(){
    this.friendService.getAllFriendsByUserId(this.authenticationService.currentUserValue.id);
  }
}
