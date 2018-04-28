import { Component, OnInit } from '@angular/core';
import { CarDetailDTO } from '../app.models';
import { ActivatedRoute, Router } from '@angular/router';
import { CarService } from '../car.service';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css']
})
export class CarComponent implements OnInit {

  private carReady: boolean;
  private sub: any;
  private car: CarDetailDTO;
  public serialNum: number;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private service: CarService) { }

  ngOnInit() {
    this.carReady = false;

    this.sub = this.route.params.subscribe(
      params => {
        this.serialNum = +params['serialNum'];
        /*this.service.getCarBySerialNum(this.serialNum).subscribe(
          currentCar => {
            this.car = currentCar;
            console.log(this.car);
            this.carReady = true;
          }, err => {
            this.carReady = false;
            console.log('Unable to get car!');
        });*/
        this.service.getCar(this.serialNum).subscribe(
          currentCar => {
            this.car = currentCar[0];
            this.carReady = true;
          }, err => {
            this.carReady = false;
            console.log('Unable to get car!');
          }
        );
    });
  }

  getYear(date) {
    const dateArray = date.split('.');
    return dateArray[2];
  }

}
