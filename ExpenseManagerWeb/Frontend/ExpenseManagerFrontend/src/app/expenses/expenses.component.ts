import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {FormBuilder} from '@angular/forms';
import {AlertService} from '../_services/alert.service';
import {AuthenticationService} from '../_services/authentication.service';
import {TransactionService} from '../_services/transaction.service';
import {first} from 'rxjs/operators';
import {Transaction} from '../_models/transaction';

interface AccountStatement {
    income: number;
    expense: number;
  }

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {

  // @ts-ignore
  accountStatement: AccountStatement = {};

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private authenticationService: AuthenticationService,
    private transactionService: TransactionService
  ) {
    this.accountStatement.income = 0;
    this.accountStatement.expense = 0;
  }

  ngOnInit(): void {
    this.transactionService
      .getAccountStatement(this.authenticationService.currentUserValue.id)
      .pipe(first())
      .subscribe(
        data => {
          this.accountStatement = data;
          },
        error => {
          this.alertService.error(error);
        });
  }

  /*
  calculateTotalIncome(): void{
    this.totalIncome = this.income.reduce((sum, current) => sum += current.amount, 0);
    console.log(this.totalIncome);
  }

  calculateTotalExpense(): void{
    this.totalExpense = this.expenses.reduce((sum, current) => sum += current.amount, 0);
    console.log(this.totalExpense);
  }

  separateTransactions(): void {
    this.income = this.transactions.filter(item => item.type === 'Income');
    this.expenses = this.transactions.filter(item => item.type === 'Expense');
  }
  */
}
