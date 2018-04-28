import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { RouterTestingModule } from '@angular/router/testing';
import { CarService } from '../car.service';
import { AppComponent } from '../app.component';
import { ConnectionBackend, RequestOptions, BaseRequestOptions, Http } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { mockService } from '../MockServices/mockCar.service';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;



  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        ReactiveFormsModule,
        RouterTestingModule
      ],
      declarations: [ LoginComponent ],
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
      Http,
      {
        provide: AppComponent,
        useValue: AppComponent
      }]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a defined component', () => {
    expect(component).toBeDefined();
  });

  it('form control should be initialized', () => {
    expect(component.myForm).toBeDefined();
  });

  it('should call submit() function for carsale', () => {
    spyOn(component, 'submit').and.callThrough();
    component.submit();
    expect(component.submit).toHaveBeenCalled();
  });
});
