/**
 * Method used for reference from https://ryanchenkie.com/angular-authentication-using-route-guardss
 */

import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import * as decode from 'jwt-decode';
import { UserInfoDTO } from '../app.models';
import { CarService } from '../car.service';

@Injectable()
export class RoleService implements CanActivate {

  constructor(public auth: AuthService,
    public router: Router,
    public service: CarService) {}

  private roleClaims = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
  private emailClaims = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';

  canActivate(route: ActivatedRouteSnapshot): boolean {
    // this will be passed from the route config
    // on the data property
    const expectedRole = route.data.expectedRole;

    const token = localStorage.getItem('token');
    if (token) {
      // decode the token to get its payload
      const tokenPayload = decode(token);

      if (this.auth.isAuthenticated() || tokenPayload[this.roleClaims] !== expectedRole) {
        console.log('not authenticated!');
        this.router.navigate(['../']);
        return false;
      }
      console.log('authenticated!');
      return true;
    }
    this.router.navigate(['../']);
    return false;
  }

  getUserInfo(): UserInfoDTO {
    const token = localStorage.getItem('token');
    const tokenPayload = decode(token);

    // tslint:disable-next-line:prefer-const
    let userInfo = new UserInfoDTO();
    userInfo.email = tokenPayload[this.emailClaims];
    userInfo.id = +localStorage.getItem('id');

    // this.service.getCarSaleIDByEmail(userInfo.email);

    return userInfo;
  }
}
