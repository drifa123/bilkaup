import { TestBed, inject } from '@angular/core/testing';

import { RoleService } from './role.service';
import { MockRoleService } from '../MockServices/mock-role.service';
import { MockAuthService } from '../MockServices/mock-auth.service';
import { MockCarService } from '../MockServices/mockCar.service';
import { ActivatedRouteSnapshot } from '@angular/router';

describe('RoleService', () => {
  let mockService, mockAuthService, mockCarService, routeSnapShot;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [{
        provide: RoleService,
        useValue: MockRoleService
      }]
    });
  });

  beforeEach(() => {
    mockAuthService = new MockAuthService();
    mockCarService = new MockCarService();
    mockService = new MockRoleService(mockAuthService, mockCarService);
    routeSnapShot = new ActivatedRouteSnapshot();
  });

  it('should be created', inject([RoleService], (service: RoleService) => {
    expect(service).toBeTruthy();
  }));

  it('should call function canActivate()', () => {
    spyOn(mockService, 'canActivate');
    mockService.canActivate(routeSnapShot);
    expect(mockService.canActivate).toHaveBeenCalled();
  });

  it('should call function getUserInfo()', () => {
    spyOn(mockService, 'getUserInfo');
    mockService.getUserInfo();
    expect(mockService.getUserInfo).toHaveBeenCalled();
  });
});
