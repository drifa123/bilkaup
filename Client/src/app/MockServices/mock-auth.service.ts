import { Injectable } from '@angular/core';

@Injectable()
export class MockAuthService {

  jwtHelper: any;
  constructor() { }

  public isAuthenticated(): boolean {

    const token = localStorage.getItem('token');

    // Check whether the token is expired and return
    // true or false
    return this.jwtHelper.isTokenExpired(token);
  }
}
