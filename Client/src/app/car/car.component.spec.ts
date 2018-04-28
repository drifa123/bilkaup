import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CarComponent } from './car.component';
import { Component, OnInit } from '@angular/core';
import { CarDetailDTO } from '../app.models';
import { ActivatedRoute, Router } from '@angular/router';
import { CarService } from '../car.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { MockCarService, mockService } from '../MockServices/mockCar.service';
import { ConnectionBackend, RequestOptions, BaseRequestOptions, Http } from '@angular/http';
import { MockBackend } from '@angular/http/testing';

describe('CarComponent', () => {
  let component: CarComponent;
  let fixture: ComponentFixture<CarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CarComponent ],
      imports: [
        FormsModule,
        ReactiveFormsModule,
        RouterTestingModule
      ],
      providers: [{
        provide: CarService,
        useValue: mockService
      }, {
        provide: ConnectionBackend,
        useClass: MockBackend
      },
      {
        provide: RequestOptions,
        useClass: BaseRequestOptions
      },
      Http
    ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should get car', () => {
    component.serialNum = 1;
    expect(component).toBeTruthy();
  });

  it('should call getYear', () => {
    spyOn(component, 'getYear').and.callThrough();
    component.getYear('01.01.1990');
    expect(component.getYear).toHaveBeenCalled();
  });
});
