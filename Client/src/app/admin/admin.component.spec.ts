import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminComponent } from './admin.component';
import { Http, ConnectionBackend, RequestOptions, BaseRequestOptions } from '@angular/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { CarService } from './../car.service';
import { By } from '@angular/platform-browser'; // for selecting elements
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, DebugElement, ViewContainerRef, OnInit } from '@angular/core';
import { MockCarService, mockService } from '../MockServices/mockCar.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { CarSaleViewModel, CarSaleDTO, CarDetailDTO } from '../app.models';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { RoleService } from '../AuthenticationServices/role.service';
import { MockRoleService } from '../MockServices/mock-role.service';
import { MockBackend } from '@angular/http/testing';

describe('AdminComponent', () => {
  let component: AdminComponent;
  let fixture: ComponentFixture<AdminComponent>;
  let mockCarService;

  beforeEach((() => {
    TestBed.configureTestingModule({ // creates a dynamic module
      imports: [
        RouterTestingModule,
        FormsModule,
        ReactiveFormsModule
      ],
      declarations: [ AdminComponent ],
      providers: [{
        provide: CarService,
        useValue: mockService
      }, {
        provide: RoleService,
        useValue: MockRoleService
      }, {
        provide: ConnectionBackend,
        useClass: MockBackend
      }, {
        provide: RequestOptions,
        useClass: BaseRequestOptions
      }, CarService,
      Http]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    mockCarService = new MockCarService();
    component.carSales = [new CarSaleDTO()];
    const bla =  {phoneNum: '', name: '', ssn: '', address: '', webpage: '', email: '', id: 1, accepted : false, active: false};
    const bla2 =  {phoneNum: '', name: '', ssn: '', address: '', webpage: '', email: '', id: 1, accepted : true, active: true};
    component.carSales.push(bla);
    component.carSales.push(bla2);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a defined component', () => {
    expect(component).toBeDefined();
  });

  it('sellers object should be defined', function() {
    expect(component.carSales).toBeDefined();
  });

  it('should call acceptCarSale() function', () => {
    spyOn(component, 'acceptCarSale').and.callThrough();
    const carsale = {
      email: 'email',
    };
    component.acceptCarSale(carsale);
    expect(component.acceptCarSale).toHaveBeenCalled();
  });
  it('should call getWaitingCount', () => {
    spyOn(component, 'getWaitingCount').and.callThrough();
    component.getWaitingCount();
    expect(component.getWaitingCount).toHaveBeenCalled();
  });
  it('should call registerCarSale', () => {
    spyOn(component, 'registerCarSale').and.callThrough();
    component.registerCarSale({email: 'email'});
    expect(component.registerCarSale).toHaveBeenCalled();
  });
  it('should call revokeCarSale', () => {
    spyOn(component, 'revokeCarSale').and.callThrough();
    component.revokeCarSale({email: 'email'});
    expect(component.revokeCarSale).toHaveBeenCalled();
  });
  it('should call denyCarSale', () => {
    spyOn(component, 'denyCarSale').and.callThrough();
    component.denyCarSale({email: 'email'});
    expect(component.denyCarSale).toHaveBeenCalled();
  });

});

