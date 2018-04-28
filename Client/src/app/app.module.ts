import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { CarService } from './car.service';
import { RouterModule } from '@angular/router';
import { RegisterCarSaleComponent } from './register-car-sale/register-car-sale.component';
import { AdminComponent } from './admin/admin.component';
import { LoginComponent } from './login/login.component';
import { AuthGuardService } from './AuthenticationServices/auth-guard.service';
import { AuthService } from './AuthenticationServices/auth.service';
import { RoleService } from './AuthenticationServices/role.service';
import { JwtHelper } from './AuthenticationServices/jwt-helper.service';
import { RoleService as Role } from './AuthenticationServices/role.service';
import { CarsaleComponent } from './carsale/carsale.component';
import { AddCarComponent } from './add-car/add-car.component';
import { SearchComponent } from './search/search.component';
import { CarComponent } from './car/car.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { CommonModule } from '@angular/common';


@NgModule({
  declarations: [
    AppComponent,
    RegisterCarSaleComponent,
    AdminComponent,
    LoginComponent,
    CarsaleComponent,
    AddCarComponent,
    SearchComponent,
    CarComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    NgSelectModule,
    CommonModule,
    RouterModule.forRoot([{
      path: '',
      component: SearchComponent
      }, {
        path: 'register',
        component: RegisterCarSaleComponent
      }, {
        path: 'admin',
        component: AdminComponent,
        canActivate: [Role],
        data: {
          expectedRole: 'Admin'
        }
      }, {
        path: 'login',
        component: LoginComponent
      }, {
        path: 'carsale',
        component: CarsaleComponent,
        canActivate: [Role],
        data: {
          expectedRole: 'Carsale'
        }
      }, {
        path: 'addcar',
        component: AddCarComponent,
        canActivate: [Role],
        data: {
          expectedRole: 'Carsale'
        }
      }, {
        path: 'car/:serialNum',
        component: CarComponent
      }])
  ],
  providers: [
    CarService,
    AuthGuardService,
    AuthService,
    RoleService,
    JwtHelper
  ],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
    NO_ERRORS_SCHEMA
  ]
})
export class AppModule { }
