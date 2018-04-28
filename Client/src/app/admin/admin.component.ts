import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router, ActivatedRouteSnapshot } from '@angular/router';
import { CarService } from '../car.service';
import { CarSaleDTO, LoginDTO, RegisterViewModel } from '../app.models';
import { RoleService } from '../AuthenticationServices/role.service';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import * as decode from 'jwt-decode';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})

export class AdminComponent implements OnInit {

  public carSales: CarSaleDTO[];
  private token: string;
  private access: boolean;
  private waitingCount: number;
  private activeCount: number;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private service: CarService,
    private roleService: RoleService,
    private fb: FormBuilder,
    private ref: ChangeDetectorRef) { }

  ngOnInit() {
    this.carSales = new Array<CarSaleDTO>();
    this.waitingCount = 0;
    this.activeCount = 0;

    this.service.getAdminCarSales().subscribe(
      carSales => {
        this.carSales = carSales;
        // Might be done in a better way in HTML
        this.getWaitingCount();
      }, err => {
        console.log('Could not load waiting carsales');
    });
  }

  getWaitingCount() {
    for (const value of this.carSales) {
      if (value.accepted === false) {
          this.waitingCount++;
      } else if (value.accepted === true && value.active === true) {
        this.activeCount++;
      }
    }
  }

  // Registers a carsale but does not give it access to Bilkaup
  registerCarSale(carSale) {
    const regInfo = new RegisterViewModel();
      regInfo.email = carSale.email;
      regInfo.role = 'Carsale';

    const reply = this.service.registerCarSale(carSale).subscribe(
      carSales => {
        console.log('Carsale was registered');
      }, err => {
        console.log('Could not register carSale');
    });
  }

  // Gives a carsale access to Bilkaup
  acceptCarSale(carSale) {
    // moved all functionality to registerCarSale because functions were not in sync
    console.log(carSale);
    const regInfo = new RegisterViewModel();
      regInfo.email = carSale.email;
      regInfo.role = 'Carsale';

    console.log(regInfo);
    this.service.registerCarSale(regInfo).subscribe(
      carSales => {
        this.carSales = carSales;
        console.log('carsale was registered');
      }, err => {
        console.log('could not register carSale');
    });
  }

  revokeCarSale(carSale) {
    const reply = this.service.revokeCarSale(carSale).subscribe(
      carSales => {
        console.log('This carsale has been revoked');
        this.carSales = carSales;
      }, err => {
        console.log('Could not revoke this carsales access');
    });
  }

  denyCarSale(carSale) {
    this.service.denyCarSale(carSale).subscribe(
      carSales => {
        console.log('carsale was denied and deleted');
        this.carSales = carSales;
      }, err => {
        console.log('could not delete carSale');
    });
  }
}
