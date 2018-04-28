/**
 * Method used for reference from https://ryanchenkie.com/angular-authentication-using-route-guardss
 */
import { TestBed, inject } from '@angular/core/testing';
import { AuthGuardService } from './auth-guard.service';
import { AuthService } from './auth.service';
import { MockAuthService } from '../MockServices/mock-auth.service';
import { MockAuthGuardService } from '../MockServices/mock-auth-guard.service';
import { Router } from '@angular/router';

describe('AuthGuardService', () => {
  let mockService, mockAuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [{
        provide: AuthService,
        useValue: MockAuthService
      }, {
        provide: AuthGuardService,
        useValue: MockAuthGuardService
      }]
    });
    TestBed.compileComponents();
  });

  beforeEach(() => {
    mockAuthService = new MockAuthService();
    mockService = new MockAuthGuardService(mockAuthService);
  });

  it('should be created', inject([AuthGuardService], (service: AuthGuardService) => {
    expect(service).toBeTruthy();
  }));

  it('should call function canActivate()', () => {
    spyOn(mockService, 'canActivate');
    mockService.canActivate();
    expect(mockService.canActivate).toHaveBeenCalled();
  });
});
