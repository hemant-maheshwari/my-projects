import { Injectable } from '@angular/core';
import { HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { delay, mergeMap, materialize, dematerialize } from 'rxjs/operators';
import {Transaction} from '../_models/transaction';

// array in local storage for registered users
let users = JSON.parse(localStorage.getItem('users')) || [];

const transactions = JSON.parse(localStorage.getItem('transactions')) || [];

const friends = JSON.parse(localStorage.getItem('friends')) || [];

@Injectable()
export class FakeBackendInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const { url, method, headers, body } = request;

    // wrap in delayed observable to simulate server api call
    return of(null)
      .pipe(mergeMap(handleRoute))
      .pipe(materialize()) // call materialize and dematerialize to ensure delay even if an error is thrown (https://github.com/Reactive-Extensions/RxJS/issues/648)
      .pipe(delay(500))
      .pipe(dematerialize());

    function handleRoute() {
      switch (true) {
        case url.endsWith('/users/authenticate') && method === 'POST':
          return authenticate();
        case url.endsWith('/users/register') && method === 'POST':
          return register();
        case url.endsWith('/users/') && method === 'GET':
          return getUsers();
        case url.match(/\/users\/\d+$/) && method === 'DELETE':
          return deleteUser();
        case url.endsWith('/users/updateAccount') && method === 'POST':
          return updateUser();
        case url.endsWith('/users/checkEmailExistence') && method === 'POST':
          return checkEmailExistence();
        case url.endsWith('/users/updatePassword') && method === 'POST':
          return updatePassword();
        case url.endsWith('/transactions/create') && method === 'POST':
          return createTransaction();
        case url.endsWith('/transactions/findAllByUserId') && method === 'POST':
          return findAllTransactionsByUserId();
        case url.endsWith('/users/getUsersBySearch') && method === 'POST':
          return getUsersFromSearch();
        default:
          // pass through any requests not handled above
          return next.handle(request);
      }
    }

    // route functions

    function authenticate() {
      const { username, password } = body;
      const user = users.find(x => x.username === username && x.password === password);
      if (!user) { return error('Username or password is incorrect'); }
      return ok({
        id: user.id,
        username: user.username,
        firstName: user.firstName,
        lastName: user.lastName,
        phoneNumber: user.phoneNumber,
        email: user.email,
        password: user.password,
        token: 'fake-jwt-token'
      });
    }

    function updateUser() {
      if (!isLoggedIn()) { return unauthorized(); }
      const updatedUser = body;
      for (const index in users){
        if (users[index].id === updatedUser.id) {
          users[index] = updatedUser;
          break;
        }
      }
      localStorage.setItem('users', JSON.stringify(users));
      return ok();
    }

    function updatePassword(){
      const updatedInfo = body;
      for (const index in users){
        if (users[index].id === updatedInfo.id) {
          users[index].password = updatedInfo.newPassword;
          break;
        }
      }
      localStorage.setItem('users', JSON.stringify(users));
      return ok();
    }

    function checkEmailExistence() {
      const email = body;
      const foundUser =  users.find(x => x.email === email);
      return ok(foundUser.id);
    }

    function getUsersFromSearch() {
      console.log(body);
      return ok();
    }

    function register() {
      const user = body;

      if (users.find(x => x.username === user.username)) {
        return error('Username "' + user.username + '" is already taken');
      }

      user.id = users.length ? Math.max(...users.map(x => x.id)) + 1 : 1;
      users.push(user);
      localStorage.setItem('users', JSON.stringify(users));

      return ok();
    }

    function createTransaction() {
      const transaction = body;
      transaction.id = transactions.length ? Math.max(...transactions.map(x => x.id)) + 1 : 1;
      transactions.push(transaction);
      localStorage.setItem('transactions', JSON.stringify(transactions));
      return ok();
    }

    function findAllTransactionsByUserId(){
      const userId = body;
      let userTransactions = [];
      for (const index in transactions){
        if ( transactions[index].userId === userId){
             userTransactions.push(transactions[index]);
        }
      }
      return ok(userTransactions);
    }

    function getUsers() {
      if (!isLoggedIn()) { return unauthorized(); }
      return ok(users);
    }

    function deleteUser() {
      if (!isLoggedIn()) { return unauthorized(); }

      users = users.filter(x => x.id !== idFromUrl());
      localStorage.setItem('users', JSON.stringify(users));
      return ok();
    }

    // helper functions

    function ok(body?) {
      return of(new HttpResponse({ status: 200, body }));
    }

    function error(message) {
      return throwError({ error: { message } });
    }

    function unauthorized() {
      return throwError({ status: 401, error: { message: 'Unauthorized' } });
    }

    function isLoggedIn() {
      return headers.get('Authorization') === 'Bearer fake-jwt-token';
    }

    function idFromUrl() {
      const urlParts = url.split('/');
      return parseInt(urlParts[urlParts.length - 1]);
    }
  }
}

export const fakeBackendProvider = {
  // use fake backend in place of Http service for backend-less development
  provide: HTTP_INTERCEPTORS,
  useClass: FakeBackendInterceptor,
  multi: true
};

// The fake backend provider enables the example to run without a backend / backendless,
// it uses HTML5 local storage for storing registered user data and provides fake implementations
// for authentication and CRUD methods, these would be handled by a real api and database in a
// production application.

// It's implemented using the HttpInterceptor class that was introduced in Angular 4.3 as
// part of the new HttpClientModule. By extending the HttpInterceptor class you can create a custom
// interceptor to modify http requests before they get sent to the server.
// In this case the FakeBackendInterceptor intercepts certain requests based on their
// URL and provides a fake response instead of going to the server.
