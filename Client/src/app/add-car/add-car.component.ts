import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CarService } from '../car.service';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { CarViewModel, CarDTO, WheelDTO, FuelTypeDTO, DriveSteeringDTO } from '../app.models';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styleUrls: ['./add-car.component.css']
})

export class AddCarComponent implements OnInit {

  myForm: FormGroup;
  carChange: CarViewModel;
  car: CarViewModel;
  carReady: boolean;
  wheels: WheelDTO[];
  fuelTypes: FuelTypeDTO[];
  driveSteeringInfos: DriveSteeringDTO[];

  constructor(private router: Router,
    private route: ActivatedRoute,
    private service: CarService,
    private fb: FormBuilder) { }


  ngOnInit() {
    this.carReady = false;

    this.service.getWheels().subscribe(
      wheel => {
        this.wheels = wheel;
      }, err => {
        console.log('Unable to get wheels');
    });

    this.service.getFuelTypes().subscribe(
      fuel => {
        this.fuelTypes = fuel;
      let i;
      for (i = 0; i < fuel.length; i++) {
        console.log('Fuel: ' + fuel[i].fuel);
      }
        // this.router.navigate(['../']);
      }, err => {
        console.log('Unable to get FUEL');
    });

    this.service.getDriveSteeringInfos().subscribe(
      driveSteering => {
        this.driveSteeringInfos = driveSteering;
      let i;
      for (i = 0; i < driveSteering.length; i++) {
        console.log('DriveSteering: ' + driveSteering[i].name);
      }
        // this.router.navigate(['../']);
      }, err => {
        console.log('Unable to get driveSteering');
    });


    this.myForm = this.fb.group({
      regNum: new FormControl('', Validators.required)
    });
  }

  submit() {
    if (this.carReady === false) {
      return;
    }

    const wheelInputs = document.getElementsByName('wheel');
    this.car.wheel = new Array();
    let i;
    for (i = 0; i < wheelInputs.length; i++) {
        const chk = <HTMLInputElement>wheelInputs[i];
        if (chk.checked) {
          this.car.wheel.push(+wheelInputs[i].id);
          console.log(wheelInputs[i].id);
        }
    }

    const fuelInputs = document.getElementsByName('fuel');
    this.car.fuelType = new Array();

    for (i = 0; i < fuelInputs.length; i++) {
         if ((<HTMLInputElement>fuelInputs[i]).checked) {
           this.car.fuelType.push(+fuelInputs[i].id);
           console.log('Pushing fueltype: ' + fuelInputs[i].id);
         }
     }

     const driveSteeringInputs = document.getElementsByName('driveSteering');
    this.car.driveSteering = new Array();

    for (i = 0; i < driveSteeringInputs.length; i++) {
         if ((<HTMLInputElement>driveSteeringInputs[i]).checked) {
           this.car.driveSteering.push(+driveSteeringInputs[i].id);
           console.log('Pushing driveSteering: ' + driveSteeringInputs[i].id);
         }
     }

    this.car.carSaleId = +localStorage.getItem('id');
    console.log(this.myForm.value);
    console.log(this.car);
    const reply = this.service.addCar(this.car).subscribe(
      car => {
        console.log('Car id added: ' + car);
        // this.router.navigate(['../']);
      }, err => {
        console.log('Unable to send your car');
    });
  }

  getCarFromSamgongustofa(regNum: string) {
    const reply = this.service.getCarFromSamgongustofa(regNum).subscribe(
      car => {
        this.car = new CarViewModel();
          this.car.regNum = car.fastanumer;
          this.car.manufacturer = car.tegund;
          this.car.year = car.firstskrad;
          this.car.model = car.undirtegund;
          this.car.color = car.litur;
          this.car.co2 = car.co2losun;
          this.car.weight = car.eiginthyngd;
          this.car.status = car.stada;
          this.car.nextCheckUp = car.naestaadalskodun;
          this.car.seating = 5;
          this.car.doors = 5;

          this.carReady = true;
          // this.openPage('Almennt');
      }, err => {
        console.log(err);
        console.log('Unable to get car from Samgongustofa');
      }
    );
  }

  firstLetterCaps(input: string) {
    return (!!input) ? input.charAt(0).toUpperCase() + input.substr(1).toLowerCase() : '';
  }

  openPage(pageName: string) {
    console.log('IN OPEN PAGE');
    // Hide all elements with class="tabcontent" by default */
    let i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName('tabcontent');
    for (i = 0; i < tabcontent.length; i++) {
      tabcontent[i].style.display = 'none';
    }

    // Remove the background color of all tablinks/buttons
    tablinks = document.getElementsByClassName('tablink');
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].style.backgroundColor = '';
    }

    // Show the specific tab content
    document.getElementById(pageName).style.display = 'block';
  }
}
