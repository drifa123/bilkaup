import { Component, OnInit } from '@angular/core';
import { UserInfoDTO, CarSaleDetailDTO, CarCardDTO } from '../app.models';
import { CarService } from '../car.service';
import { HostListener } from '@angular/core';

@Component({
  selector: 'app-carsale',
  templateUrl: './carsale.component.html',
  styleUrls: ['./carsale.component.css']
})
export class CarsaleComponent implements OnInit {

  public carSaleInfo: CarSaleDetailDTO;
  public carSaleReady: boolean;
  public showOptions: boolean;
  public showID: number;

  constructor(public service: CarService) { }

  ngOnInit() {
    this.carSaleReady = false;
    this.carSaleInfo = new CarSaleDetailDTO();
    this.showOptions = false;
    this.showID = 0;

    this.service.getCarSaleDetail(+localStorage.getItem('id')).subscribe(
      carSale => {
        this.carSaleInfo = carSale;
        console.log(this.carSaleInfo);
        this.carSaleReady = true;
      }, err => {
        console.log('Could not get carsale info');
    });
  }

  @HostListener('document:click', ['$event']) clickedOutside($event) {
    // here you can hide your menu
    this.showID = 0;
    this.showOptions = false;
  }

  carOptions($event: Event, serialNum: number) {
    $event.preventDefault();
    $event.stopPropagation();

    if (serialNum === this.showID) {
      this.showOptions = !this.showOptions;
    } else {
      this.showID = serialNum;
      this.showOptions = true;
    }
  }

  sold(serialNum: number) {
    this.service.sellCar(serialNum).subscribe(
      cars => {
        this.carSaleInfo.cars = cars;
      }, err => {
        console.log('Unable to sell car');
    });
  }
}
