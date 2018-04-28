import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import * as xml2js from 'xml2js';
import 'rxjs/rx';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { CarSaleViewModel, CarSaleDTO, SamgongustofaDTO, CarDTO, CarViewModel, LoginDTO, LoginViewModel, CarDetailDTO } from './app.models';
import { RegisterViewModel, CarSaleDetailDTO, FilterDTO, CarCardDTO, WheelDTO, FuelTypeDTO, DriveSteeringDTO } from './app.models';

@Injectable()
export class CarService {

  constructor(private http: Http) { }

    /*
   * To get POST request to work we followed this tutorial:
   * https://angular.io/docs/ts/latest/guide/server-communication.html#!#extract-data
   * Helper functions extractData() and handleError() are taken from that tutorial.
   */
  private extractData(res: Response) {
    const body = res.json();
    if (body.product) {
      return body.product;
    }
    return body || { };
  }

  private handleError (error: Response | any) {
    // In a real world app, we might use a remote logging infrastructure
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }

    console.error(errMsg);

    return Observable.throw(errMsg);
  }

  // Gets all cars in database
  getCars(): Observable<CarDTO[]> {
    return this.http.get('/api/car/')
      .map(response => {
        return<CarDTO[]>response.json();
      });
  }

  // Get all wheel types in database
  getWheels(): Observable<WheelDTO[]> {
    return this.http.get('/api/car/wheel')
      .map(response => {
        return<WheelDTO[]>response.json();
      });
  }

  // Get all fuel types from database
  getFuelTypes(): Observable<FuelTypeDTO[]> {
    return this.http.get('/api/car/fuelType')
      .map(response => {
        return<FuelTypeDTO[]>response.json();
      });
  }

  // Get all drive steering from database
  getDriveSteeringInfos(): Observable<DriveSteeringDTO[]> {
    return this.http.get('/api/car/driveSteering')
      .map(response => {
        return<DriveSteeringDTO[]>response.json();
      });
  }

  // Gets all search filters values
  getFilters(): Observable<FilterDTO> {
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});
    return this.http.get('/api/car/filters', options)
      .map(response => {
        return<FilterDTO>response.json();
      });
  }

  // Gets all filtered cars
  getFilteredCars(sort): Observable<CarCardDTO[]> {
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});
    headers.append('sort', sort);
    return this.http.get('/api/car/find?=' + localStorage.getItem('term'), options)
      .map(response => {
        return<CarCardDTO[]>response.json();
      });
  }
  getCar(serialNum): Observable<Object> {
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});
    return this.http.get('/api/car/find?=' + serialNum, options)
      .map(response => {
        return<Object>response.json();
      });
  }

  // Sends the new carsale down to the API
  addCarSale(newCarSale: CarSaleViewModel): Observable<number> {
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});

    return this.http.post('/api/carsale/', newCarSale, options)
      .map(this.extractData)
      .catch(this.handleError);
  }

  // Gets carsales that are waiting to be accepted
  getAdminCarSales(): Observable<CarSaleDTO[]> {
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});
    return this.http.get('/api/carsale/admin/carSales', options)
      .map(response => {
        return<CarSaleDTO[]>response.json();
      });
  }

  // Gets the details of a specific car with that ID
  getCarBySerialNum(serialNum: number): Observable<CarDetailDTO> {
    return this.http.get('/api/car/' + serialNum)
      .map(response => {
        return<CarDetailDTO>response.json();
      });
  }

  // Accepts a carsale application
  acceptCarSale(carSale: CarSaleDTO): Observable<CarSaleDTO[]> {
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});

    return this.http.put('/api/carsale/ ' + carSale.id + '/accept', options)
      .map(response => {
        return<CarSaleDTO[]>response.json();
      })
      .catch(this.handleError);
  }

  // Revoke's a carsale's access
  revokeCarSale(carSale: CarSaleDTO): Observable<CarSaleDTO[]> {
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});

    return this.http.put('/api/carsale/ ' + carSale.id + '/revoke', options)
      .map(response => {
        return<CarSaleDTO[]>response.json();
      })
      .catch(this.handleError);
  }

  // Denies a carsale application
  denyCarSale(carSale: CarSaleDTO): Observable<CarSaleDTO[]> {
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});

    return this.http.delete('/api/carsale/ ' + carSale.id, options)
    .map(response => {
      return<CarSaleDTO[]>response.json();
    })
    .catch(this.handleError);
  }

  // Registeres carsale and gives it login information
  registerCarSale(preRegister: RegisterViewModel): Observable<CarSaleDTO[]> {
    console.log('in registerCarSale');
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});

    return this.http.post('api/account/register/', preRegister, options)
      .map(response => {
        return<CarSaleDTO[]>response.json();
      })
      .catch(this.handleError);
  }

  addCar(newCar: CarViewModel): Observable<number> {
    console.log('in addCar:' + newCar);
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});
    console.log(newCar);
    return this.http.post('/api/car/', newCar, options)
      .map(response => {
        return<number>response.json();
      })
      .catch(this.handleError);
  }

  getCarFromSamgongustofa(regNum: string): Observable<SamgongustofaDTO> {
    return this.http.get('samgongustofa/' + regNum)
    .map(response => {
      let tempRes;
      xml2js.parseString(response.text(), function(err, result) {
        tempRes = result;
      });

      let temp = new SamgongustofaDTO();
      temp = tempRes['okutaeki'];

      // Map the xml object into our SamgongustofaDTO
      // tslint:disable-next-line:prefer-const
      let car = new SamgongustofaDTO();
      car.fastanumer = temp.fastanumer[0];
      car.skraningarnumer = temp.skraningarnumer[0];
      car.verksmidjunumer = temp.verksmidjunumer[0];
      car.tegund = temp.tegund[0];
      car.undirtegund = temp.undirtegund[0];
      car.litur = temp.litur[0];
      car.firstskrad = temp.firstskrad[0];
      car.stada = temp.stada[0];
      car.naestaadalskodun = temp.naestaadalskodun[0];
      car.co2losun = temp.co2losun[0];
      car.eiginthyngd = temp.eiginthyngd[0];
      console.log(car);

      return car;
    });
  }

  // Logs in carsale/admin
  login(loginInfo: LoginViewModel): Observable<LoginDTO> {
    console.log('logging in');
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});

    return this.http.post('api/account/login/', loginInfo, options)
    .map(response => {
      return<Object>response.json();
    })
    .catch(this.handleError);
  }

  // Logs out user
  logout(): Observable<Object> {
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});
    return this.http.post('api/account/logout/', options)
    .map(response => {
      return<Object>response;
    })
    .catch(this.handleError);
  }

  getCarSaleDetail(id: number): Observable<CarSaleDetailDTO> {
    return this.http.get('/api/carsale/' + id)
    .map(response => {
      return<CarSaleDetailDTO>response.json();
    });
  }

  sellCar(serialNum: number): Observable<CarDTO[]> {
    const headers = new Headers({'Content-Type': 'application/json'});
    const options = new RequestOptions({headers: headers});
    return this.http.delete('/api/car/' + serialNum, options)
    .map(response => {
      console.log(response);
      return<CarDTO[]>response.json();
    });
  }
}
