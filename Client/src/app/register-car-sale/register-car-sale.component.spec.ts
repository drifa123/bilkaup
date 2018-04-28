import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterCarSaleComponent } from './register-car-sale.component';
import { Http } from '@angular/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { CarService } from './../car.service';

import { By } from '@angular/platform-browser'; // for selecting elements
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, DebugElement, ViewContainerRef, OnInit } from '@angular/core';
import { mockService, MockCarService } from '../MockServices/mockCar.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { CarSaleViewModel } from '../app.models';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';

describe('RegisterCarSaleComponent', () => {
  let component: RegisterCarSaleComponent;
  let fixture: ComponentFixture<RegisterCarSaleComponent>;

  beforeEach((() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        FormsModule,
        ReactiveFormsModule
      ],
      declarations: [
        RegisterCarSaleComponent
      ],
      providers: [{
        provide: CarService,
        useValue: mockService
      }]
    })
    .compileComponents();
  }));

  beforeEach(() => { // tests are fired up
    fixture = TestBed.createComponent(RegisterCarSaleComponent); // creates a reference to the component
    component = fixture.componentInstance; // fires up our constructor so we have a instance at this moment
    fixture.detectChanges(); // have to call to fire up what has changed
    // fires up ngOnInit() firstly so it is on the stack first not yet called
  });

  // COMPONENT TESTS
  it('should create', () => {
    // fixture.detectChanges(); // first time ngOnInit is called
    expect(component).toBeTruthy(); // able to create a component
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a defined component', () => {
    expect(component).toBeDefined();
  });

  it('sellers object should be defined', function() {
    expect(component.myForm).toBeDefined();
  });

  it('should call submit() function', () => {
    spyOn(component, 'submit').and.callThrough();
    component.submit();
    expect(component.submit).toHaveBeenCalled();
  });
});
