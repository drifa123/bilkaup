import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { CarDTO, CarSaleDTO, CarSaleViewModel, RegisterViewModel,
  LoginViewModel, LoginDTO, CarDetailDTO, FilterDTO } from '../app.models';
import { FilterData } from '../search/filterData';

export const mockService = {
    loginDataCarsale: {
    id: 1,
    role: 'Carsale',
    token: 'crappyToken'
  },
  loginDataAdmin: {
    id: 2,
    role: 'Carsale',
    token: 'crappyToken'
  },
  filters: new FilterDTO(),
  login: function(loginInfo) {
    return { subscribe: function(fnSuccess, fnError) {
      if (loginInfo) {
          fnSuccess(mockService.loginDataCarsale);
      } else {
          fnError();
      }
    }
  }; },
  addCarSale: function() {
    return { subscribe: function(fnSuccess) {
          fnSuccess(null);
      }
    };
  },
  getCar: function(id) {
    return { subscribe: function(fnSuccess, fnError) {
      if (!id) {
        fnSuccess([{id: 1}]);
      } else {
        fnError();
      }
    }};
},
getAdminCarsales: function(id) {
  return { subscribe: function(fnSuccess, fnError) {
    fnSuccess([{id: 1, accepted : false, active: false}, {id: 1, accepted : true, active: true}]);
  }};
},
};
@Injectable()
export class MockCarService {
}
