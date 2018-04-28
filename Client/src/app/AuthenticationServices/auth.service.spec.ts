import { TestBed, inject } from '@angular/core/testing';

import { AuthService } from './auth.service';
import { MockAuthService } from '../MockServices/mock-auth.service';

describe('AuthService', () => {
  let mockService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [{
        provide: AuthService,
        useValue: MockAuthService
      }]
    });
  });

  beforeEach(() => {
    mockService = new MockAuthService();
  });

  it('should be created', inject([AuthService], (service: AuthService) => {
    expect(service).toBeTruthy();
  }));

  it('should call function isAuthenticated()', () => {
    spyOn(mockService, 'isAuthenticated');
    mockService.isAuthenticated();
    expect(mockService.isAuthenticated).toHaveBeenCalled();
  });
});
