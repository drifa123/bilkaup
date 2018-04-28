import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { RegisterCarSaleComponent } from './register-car-sale/register-car-sale.component';
import { Http } from '@angular/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { CarService } from './car.service';


describe('CarService', () => {
  // tslint:disable-next-line:prefer-const
  let component: RegisterCarSaleComponent;
  // tslint:disable-next-line:prefer-const
  let fixture: ComponentFixture<RegisterCarSaleComponent>;

  const mockService = {
    carSale : {
      id: 1,
      name : 'besta bílasalan',
      ssn : '000000-0000',
      email: 'besta@bila.is',
      phoneNum: '5555555',
      address: 'address',
      webpage: 'webpage.com'
    },
    getCars: function() {},
    addCarSale: function(newCarSale) {},
    getWaitingCarSales: function() {},
    acceptCarSale: function(carSale) {},
    registerCarSale: function(carSale) {},
    revokeCarSale: function(carSale) {}
  };
  const mockHttp = {
    get: jasmine.createSpy('get'),
    post: jasmine.createSpy('post'),
    put: jasmine.createSpy('put'),
    map: jasmine.createSpy('map')
  };

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      // declarations: [ RegisterCarSaleComponent ]
      providers: [{
        provide: CarService,
        useValue: mockService
      }, {
        provide: Http,
        useValue: mockHttp
      }]
    });
    TestBed.compileComponents();
  }));

  /*beforeEach(() => {
    fixture = TestBed.createComponent(RegisterCarSaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });*/

  it('should ...', inject([CarService], (service: CarService) => {
    expect(service).toBeTruthy();
  }));

  it('should be okay with mockHttp', () => {
    expect(mockHttp).toBeTruthy();
  });

  it('should call post in addCarSale', () => {
    mockService.addCarSale(mockService.carSale);
    // tslint:disable-next-line:no-unused-expression
    expect(mockHttp.post).toHaveBeenCalled;
  });

  it('should call get in getWaitingCarSales', () => {
    mockService.getWaitingCarSales();
    // tslint:disable-next-line:no-unused-expression
    expect(mockHttp.get).toHaveBeenCalled;
  });

  it('should call post in acceptCarSale', () => {
    mockService.acceptCarSale(mockService.carSale.id);
    // tslint:disable-next-line:no-unused-expression
    expect(mockHttp.post).toHaveBeenCalled;
  });

  it('should call put in acceptCarSale', () => {
    mockService.acceptCarSale(mockService.carSale.id);
    // tslint:disable-next-line:no-unused-expression
    expect(mockHttp.put).toHaveBeenCalled;
  });

  it('should call put in revokeCarSale', () => {
    mockService.revokeCarSale(mockService.carSale.id);
    // tslint:disable-next-line:no-unused-expression
    expect(mockHttp.put).toHaveBeenCalled;
  });

  it('should call post in registerCarSale', () => {
    mockService.registerCarSale(mockService.carSale.id);
    // tslint:disable-next-line:no-unused-expression
    expect(mockHttp.post).toHaveBeenCalled;
  });
});
