import { TestBed, inject } from '@angular/core/testing';
import { JwtHelper } from './jwt-helper.service';
import { MockJwtHelper } from '../MockServices/mock-jwt-helper.service';

describe('JwtHelper', () => {
  let mockHelper;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [{
        provide: JwtHelper,
        useValue: MockJwtHelper
      }]
    });
  });

  beforeEach(() => {
    mockHelper = new MockJwtHelper();
  });

  it('should be created', inject([JwtHelper], (service: JwtHelper) => {
    expect(service).toBeTruthy();
  }));

  it('should call function urlBase64Decode()', () => {
    spyOn(mockHelper, 'urlBase64Decode');
    mockHelper.urlBase64Decode('string');
    expect(mockHelper.urlBase64Decode).toHaveBeenCalled();
  });

  it('should call function decodeToken()', () => {
    spyOn(mockHelper, 'decodeToken');
    mockHelper.decodeToken('tokenString');
    expect(mockHelper.decodeToken).toHaveBeenCalled();
  });

  it('should call function getTokenExpirationDate()', () => {
    spyOn(mockHelper, 'getTokenExpirationDate');
    mockHelper.getTokenExpirationDate('tokenString');
    expect(mockHelper.getTokenExpirationDate).toHaveBeenCalled();
  });

  it('should call function isTokenExpired()', () => {
    spyOn(mockHelper, 'isTokenExpired');
    mockHelper.isTokenExpired('tokenString', 5);
    expect(mockHelper.isTokenExpired).toHaveBeenCalled();
  });
});
