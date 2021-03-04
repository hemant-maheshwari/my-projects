import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {AlertService} from '../_services/alert.service';
import {AuthenticationService} from '../_services/authentication.service';
import {Friend} from '../_models/friend';
import {TransactionService} from '../_services/transaction.service';
import {first} from 'rxjs/operators';
import {FriendsService} from '../_services/friends.service';

interface TransactionType {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  transactionForm: FormGroup;
  loading = false;
  submitted = false;
  friends: any[] = [];
  friendsSelect: any = [];
  incomeDescription = 'This is money you recently spent and are requesting back.';
  expenseDescription = 'This is money you are expected to pay back.';

  transactionTypes: TransactionType[] = [
    {value: 'Expense', viewValue: 'Expense'},
    {value: 'Income', viewValue: 'Income'}
  ];

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private transactionService: TransactionService,
    private friendService: FriendsService
  ) {
    this.friendsSelect = [];
  }

  ngOnInit(): void {
    this.transactionForm = this.formBuilder.group({
      title: ['', Validators.required],
      type: ['', Validators.required],
      amount: ['', [Validators.required, Validators.pattern(/^\d*\.?\d{2}$/)]],
      shares: [''],
     // picture: ['']
    });

    this.friendService
      .getAllFriendsByUserId(this.authenticationService.currentUserValue.id)
      .pipe(first())
      .subscribe(
        data => {
          this.friends = data;
          this.initializeFriendsList();
        }, error => {
          this.alertService.error(error);
        });
  }

  initializeFriendsList(): void{
    for (const friend of this.friends){
      this.friendsSelect.push({
        name: friend.firstName.concat(' ').concat(friend.lastName),
        userId: friend.userId
      });
    }
  }

  get f(): any{ return this.transactionForm.controls; }

  onSubmit(): void{
    this.submitted = true;
    this.alertService.clear();
    if (this.transactionForm.invalid){
      return;
    }
    this.loading = true;
    this.transactionService.createTransaction(this.transactionForm.value, this.authenticationService.currentUserValue.id)
      .pipe(first())
      .subscribe(
        data => {
          alert('Transaction created successfully!');
          this.transactionForm.reset();
        },
        error => {
          this.alertService.error(error);
        });
    this.loading = false;
    this.submitted = false;
  }

}
