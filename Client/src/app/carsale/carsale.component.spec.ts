import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CarsaleComponent } from './carsale.component';
import { MockRoleService } from '../MockServices/mock-role.service';
import { RoleService } from '../AuthenticationServices/role.service';
import { CarService } from '../car.service';
// import { MockCarService } from '../MockServices/mockCar.service';
import { ConnectionBackend, BaseRequestOptions, RequestOptions, Http } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';
import { AuthService } from '../AuthenticationServices/auth.service';
import { MockAuthService } from '../MockServices/mock-auth.service';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { mockService } from '../MockServices/mockCar.service';

describe('CarsaleComponent', () => {
  let component: CarsaleComponent;
  let fixture: ComponentFixture<CarsaleComponent>;

  beforeEach((() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        FormsModule,
        ReactiveFormsModule
      ],
      declarations: [ CarsaleComponent ],
      providers: [{
        provide: CarService,
        useValue: mockService
      }, {
        provide: ConnectionBackend,
        useClass: MockBackend
      }, {
        provide: RequestOptions,
        useClass: BaseRequestOptions
      },
      Http, CarService]

    })
    .compileComponents();
  }));

  beforeEach(() => {
    localStorage.setItem('id', '1');
    fixture = TestBed.createComponent(CarsaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have id in localstorage', () => {
    expect(localStorage.getItem('id')).toEqual('1');
  });

  it('should show all information about the carsale', () => {
    expect(component.carSaleInfo).toBeDefined();
  });

  it('should have carsale ready', () => {
    component.carSaleReady = true;
    expect(component.carSaleReady).toBeTruthy();
  });
  it('should call sold', () => {
    spyOn(component, 'sold').and.callThrough();
    component.sold(1);
    expect(component.sold).toHaveBeenCalled();
  });
  it('should call clickOutside', () => {
    spyOn(component, 'clickedOutside').and.callThrough();
    component.clickedOutside(null);
    expect(component.clickedOutside).toHaveBeenCalled();
  });
  it('should call carOptions', () => {
    spyOn(component, 'carOptions').and.callThrough();
    const event = new Event('bla');
    component.carOptions(event, 1);
    expect(component.carOptions).toHaveBeenCalled();
  });
  it('should call carOptions', () => {
    spyOn(component, 'carOptions').and.callThrough();
    const event = new Event('bla');
    component.showID = 1;
    component.carOptions(event, 1);
    expect(component.carOptions).toHaveBeenCalled();
  });
});
