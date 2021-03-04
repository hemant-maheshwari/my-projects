import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthenticationService} from '../_services/authentication.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    let currentUser = this.authenticationService.currentUserValue;
    if (currentUser) { //  && currentUser.token
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.id}`
        }
      });
    }

    return next.handle(request);
  }
}

// The JWT Interceptor intercepts http requests from the application to add a JWT auth token to
// the Authorization header if the user is logged in.

// It's implemented using the HttpInterceptor class that was introduced in Angular 4.3
// as part of the new HttpClientModule. By extending the HttpInterceptor class you can
// create a custom interceptor to modify http requests before they get sent to the server.

// Http interceptors are added to the request pipeline in the providers section of the app.module.ts file.
