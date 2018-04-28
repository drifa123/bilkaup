/**
 * Method used for reference from https://ryanchenkie.com/angular-authentication-using-route-guardss
 */
import { Injectable } from '@angular/core';
import { JwtHelper } from './jwt-helper.service';
import { AppComponent } from '../app.component';

@Injectable()
export class AuthService {

  constructor(public jwtHelper: JwtHelper) {}

  // ...
  public isAuthenticated(): boolean {

    const token = localStorage.getItem('token');

    // Check whether the token is expired and return
    // true or false
    const expired = this.jwtHelper.isTokenExpired(token);
    if (expired === true) {
      AppComponent.logout();
    }
    return expired;
  }

}



