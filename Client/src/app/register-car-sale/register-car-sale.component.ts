import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CarService } from '../car.service';
import { CarSaleViewModel } from '../app.models';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';

@Component({
  selector: 'app-register-car-sale',
  templateUrl: './register-car-sale.component.html',
  styleUrls: ['./register-car-sale.component.css']
})
export class RegisterCarSaleComponent implements OnInit {

  newCarSale: CarSaleViewModel;
  myForm: FormGroup;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private service: CarService,
    private fb: FormBuilder) { }

  ngOnInit() {
    this.myForm = this.fb.group({
      name: new FormControl('', Validators.required),
      ssn: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      phoneNum: new FormControl('', Validators.required),
      address: new FormControl(),
      webpage: new FormControl()
    });
  }

  submit() {
    console.log(this.myForm.value);

    const reply = this.service.addCarSale(this.myForm.value).subscribe(
      carSale => {
        this.router.navigate(['../']);
      }, err => {
        console.log('Unable to send your request to register');
    });
  }

}
