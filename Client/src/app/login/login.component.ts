import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CarService } from '../car.service';
import { AppComponent } from '../app.component';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import * as decode from 'jwt-decode';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  myForm: FormGroup;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private service: CarService,
    private fb: FormBuilder,
    private app: AppComponent) { }

  ngOnInit() {

      this.myForm = this.fb.group({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      rememberMe: new FormControl(false)
    });
    this.app.myPage = '';
  }

  submit() {

    const reply = this.service.login(this.myForm.value).subscribe(
      loginInfo => {
        console.log('LOGGED IN!');
        console.log(loginInfo.id);
        localStorage.setItem('token', loginInfo.token);
        localStorage.setItem('user', 'bilkaup@gmail.com');
        localStorage.setItem('id', loginInfo.id.toString());
        this.app.loggedIn = true;
        if (loginInfo.role === 'Admin') {
          this.app.myPage = '../admin';
        } else if (loginInfo.role === 'Carsale') {
          this.app.myPage = '../carsale';
        }
        localStorage.setItem('myPage', this.app.myPage);
        this.router.navigate([this.app.myPage]);
      }, err => {
        console.log('Unable to log in!');
      }
    );
  }

}
