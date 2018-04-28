import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { CarService } from './car.service';
import { CarDTO } from './app.models';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';


@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
    static logout: any;
    constructor(
        private router: Router,
        private service: CarService) { }
        public loggedIn: boolean;
        public myPage: string;


    ngOnInit() {
        this.loggedIn = false;
        this.myPage = localStorage.getItem('myPage');
        if (localStorage.getItem('token')) {
            this.loggedIn = true;
        }
    }

    public logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        localStorage.removeItem('id');
        localStorage.removeItem('myPage');
        this.loggedIn = false;
        this.myPage = '';
        const reply = this.service.logout().subscribe(
            logoutInfo => {
            }
        );
        this.router.navigate(['']);
    }
}
