import { Injectable } from '@angular/core';
import { MockAuthService } from './mock-auth.service';
import { Router } from '@angular/router';

@Injectable()
export class MockAuthGuardService {

  constructor(public auth: MockAuthService) {}

  canActivate(): boolean {
    if (!this.auth.isAuthenticated()) {
      return false;
    }
    return true;
  }
}
