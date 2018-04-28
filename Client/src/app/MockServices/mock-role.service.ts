import { Injectable } from '@angular/core';
import { MockAuthService } from './mock-auth.service';
import { Router, ActivatedRouteSnapshot } from '@angular/router';
import { MockCarService } from './mockCar.service';
import { UserInfoDTO } from '../app.models';
import * as decode from 'jwt-decode';

@Injectable()
export class MockRoleService {

  constructor(public auth: MockAuthService,
    public service: MockCarService) {}

  private roleClaims = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
  private emailClaims = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';

  canActivate(route: ActivatedRouteSnapshot): boolean {
    console.log('in role service');
    // this will be passed from the route config
    // on the data property
    const expectedRole = route.data.expectedRole;

      const token = localStorage.getItem('token');
      // decode the token to get its payload
      const tokenPayload = decode(token);

      if (this.auth.isAuthenticated() || tokenPayload[this.roleClaims] !== expectedRole) {
        console.log('not authenticated!');
        return false;
      }
      console.log('authenticated!');
      return true;
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
