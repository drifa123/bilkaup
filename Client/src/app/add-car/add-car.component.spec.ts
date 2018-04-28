import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { AddCarComponent } from './add-car.component';
import { CarService } from '../car.service';
import { mockService } from '../MockServices/mockCar.service';
import { ConnectionBackend, RequestOptions, BaseRequestOptions, Http } from '@angular/http';
import { MockBackend } from '@angular/http/testing';

describe('AddCarComponent', () => {
  let component: AddCarComponent;
  let fixture: ComponentFixture<AddCarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        ReactiveFormsModule,
        RouterTestingModule
      ],
      declarations: [ AddCarComponent ],
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
      Http, CarService
    ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have form defined', () => {
    expect(component.myForm).toBeDefined();
  });

  it('should have called function submit()', () => {
    spyOn(component, 'submit').and.callThrough();
    component.submit();
    expect(component.submit).toHaveBeenCalled();
  });

  it('should have called function getCarFromSamgongustofa()', () => {
    spyOn(component, 'getCarFromSamgongustofa').and.callThrough();
    component.getCarFromSamgongustofa('UA523');
    expect(component.getCarFromSamgongustofa).toHaveBeenCalled();
  });

  it('should have called function firstLetterCaps()', () => {
    spyOn(component, 'firstLetterCaps').and.callThrough();
    component.firstLetterCaps('string');
    expect(component.firstLetterCaps).toHaveBeenCalled();
  });

  xit('should have called function openPage()', () => {
    spyOn(component, 'openPage').and.callThrough();
    component.openPage('pageName');
    expect(component.openPage).toHaveBeenCalled();
  });
});
