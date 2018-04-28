import { Component, OnInit, SchemaMetadata, NO_ERRORS_SCHEMA } from '@angular/core';
import { CarDTO, CarDetailDTO, FilterDTO, ManufacturerDTO, CarCardDTO } from '../app.models';
import { ActivatedRoute, Router } from '@angular/router';
import { CarService } from '../car.service';
import { forEach } from '@angular/router/src/utils/collection';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs/Observable';
import { FilterData, sortFilters } from './filterData';
import { map } from 'rxjs/operator/map';
import { FormsModule, ReactiveFormsModule, SelectControlValueAccessor } from '@angular/forms';
import { NgSelectModule, NgSelectComponent } from '@ng-select/ng-select';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})

export class SearchComponent implements OnInit {

  title = 'Nýjir bílar á skrá';

  // private car: CarDTO[];
  public cars: CarCardDTO[];
  public filters: FilterDTO;
  public filterString: string;
  public advancedSearch: boolean;
  public selected;
  public hideSearch;
  public sortFilters = sortFilters;
  public sort;
  public filtersReady: boolean;
  public extraFeatures;

  constructor(
      private route: ActivatedRoute,
      private service: CarService,
      private router: Router) { }

  ngOnInit() {
    this.sort = this.sortFilters[0];
    this.hideSearch = false;
    this.filterString = '';
    this.advancedSearch = false;
    this.filtersReady = false;
    this.extraFeatures = false;
    // check if we have filters saved in localStorage
    // TODO, check if this works under all circumstances!
    // Filters are valid in localstorage for three days 300000000

    if (localStorage.getItem('filters') && parseInt(localStorage.getItem('filterTime'), null) + 300000000 > Date.now()) {
      this.filters = JSON.parse(localStorage.getItem('filters'));
      this.filtersReady = true;
      console.log('filter valid');
    } else {
      // if we don't have filters saved in local storage, we get them from API
      this.service.getFilters().subscribe(
        filters => {
          // save our filters to local storage
          this.filters = filters;
          this.setFilterData();
          this.setDefaultFilters();
          localStorage.setItem('filterTime', Date.now().toString());
          localStorage.setItem('filters', JSON.stringify(this.filters));
          console.log(this.filters);
          this.filtersReady = true;
        }, err => {
          console.log('could not load data');
        });
    }
    this.getFilteredCars();
  }
  getCarDetailPage(c) {
    console.log(c.serialNumber);
    this.router.navigate(['../car/' + c.serialNumber]);
  }

  setSort(s) {
    this.sort = s;
    this.getFilteredCars();
  }

  setFilterData() {
    const year = (new Date()).getFullYear();
    this.filters.years = [];
    for (let i = year; i > 1950; i--) {
      this.filters.years.push(i);
    }

    this.filters.prices = FilterData.prices;
    this.filters.milages = FilterData.milages;
    this.filters.extraFeatures = FilterData.extraFeatures;
    this.filters.fuelTypes = FilterData.fuelTypes;
    this.filters.doors = FilterData.doors;
    this.filters.colors = FilterData.colors;
    this.filters.transmissions = FilterData.transmissions;
    this.filters.seating = FilterData.seating;
  }

  setDefaultFilters() {
    this.filters.priceFrom = null;
    this.filters.priceTo = null;
    this.filters.milageFrom = null;
    this.filters.milageTo = null;
    this.filters.yearFrom = null;
    this.filters.yearTo = null;
  }

  getFilteredCars() {
    this.service.getFilteredCars(this.sort.value).subscribe(
      cars => {
        this.cars = cars;
      }, err => {
        console.log('could not load data');
    });
  }

  // Allir filterar eiga ser bool breytu sem byrjar á því að vera false, en við það að
  // haka við hana verður hún true
  setFilter(f) {
    if (this.selected != null) {

      this.selected.selected = !this.selected.selected;
      this.selected = null;
      // tslint:disable-next-line:no-unused-expression
      this.constructFilterString();
    }
    if (f !== undefined) {
      if (FilterData.transmissions[0].name === f.name || FilterData.transmissions[1].name === f.name) {
        this.clearUnselected(this.filters.transmissions, f);
      }
      for (const i in FilterData.fuelTypes) {
        if (FilterData.fuelTypes[i].name === f.name) {
          this.clearUnselected(this.filters.fuelTypes, f);
        }
      }
      f.selected = !f.selected;
      this.constructFilterString();
    }
  }

  clearUnselected(feature, filter) {
    for (const i in feature) {
      if (feature[i] !== filter) {
        feature[i].selected = false;
      }
    }
  }

  // Toggles between whether to hide the search window
  toggleSearch() {
    this.hideSearch = !this.hideSearch;
  }

  // Pushes the search window out of sight and back
  getStyle() {
    if (this.hideSearch === true) {
      return '-20em';
    } else {
      return '0em';
    }
  }

  constructFilterString() {
    this.filterString = '';
    let manufacturerString = '';
    // String construction for manufacturers and model
    // tslint:disable-next-line:forin
    for (const i in this.filters.manufacturers) {
      let hasModel = false;
      for (const j in this.filters.manufacturers[i].models) {
        if (this.filters.manufacturers[i].models[j].selected === true) {
          if (manufacturerString.length > 0) {
            manufacturerString += ' OR ';
          }
          manufacturerString += '(' + this.filters.manufacturers[i].name + ' AND '
          + this.filters.manufacturers[i].models[j].name + ')';
          hasModel = true;
        }
      }
      if (hasModel === false && this.filters.manufacturers[i].selected === true) {
        if (manufacturerString.length > 0) {
          manufacturerString += ' OR ';
        }
        manufacturerString += this.filters.manufacturers[i].name;
      }
    }

    // set manufacturer string to our filter string
    if (manufacturerString.length > 0) {
      this.filterString += '(' + manufacturerString + ')';
    }

    // string construction for transmission
    for (const t in this.filters.transmissions) {
      if (this.filters.transmissions[t].selected === true) {
        this.checkIfFirstEntry();
        this.filterString += ('(transmission: ' + this.filters.transmissions[t].name + ')');
      }
    }

    // string construction for fuel types
    for (const t in this.filters.fuelTypes) {
      if (this.filters.fuelTypes[t].selected === true) {
        this.checkIfFirstEntry();
        this.filterString += ('(fuelTypes: ' + this.filters.fuelTypes[t].name + ')');
      }
    }

    // string construction for extra features
    let extraFeatureString = '';
    for (const t in this.filters.extraFeatures) {
      if (this.filters.extraFeatures[t].selected === true) {
        this.checkIfFirstEntry();
        if (extraFeatureString.length > 0) {
          extraFeatureString += ' AND ';
        }
        extraFeatureString += ('(extraFeatures: ' + this.filters.extraFeatures[t].name + ')');
      }
    }
    if (extraFeatureString.length > 0) {
      this.filterString += '(' + extraFeatureString + ')';
    }

    // string construction for doors
    this.stringConstructMultiSelect(this.filters.doors, 'doors');

    // string construction for colors
    this.stringConstructMultiSelect(this.filters.colors, 'color');

    // string construction for seating
    this.stringConstructMultiSelect(this.filters.seating, 'seating');

    // string construction for price to and from
    if (this.filters.priceFrom != null) {
      this.checkIfFirstEntry();
      this.filterString += ('(price:>=' + this.filters.priceFrom + ')');
    }

    if (this.filters.priceTo != null) {
      this.checkIfFirstEntry();
      this.filterString += ('(price:<=' + this.filters.priceTo + ')');
    }

    // string construction for year to and from
    if (this.filters.yearFrom != null) {
      this.checkIfFirstEntry();
      this.filterString += ('(year:>=' + this.filters.yearFrom + ')');
    }

    if (this.filters.yearTo != null) {
      this.checkIfFirstEntry();
      this.filterString += ('(year:<=' + this.filters.yearTo + ')');
    }

    // string construction for milageFrom and milageTo
    if (this.filters.milageFrom != null) {
      this.checkIfFirstEntry();
      this.filterString += ('(year:>=' + this.filters.milageFrom + ')');
    }

    if (this.filters.milageTo != null) {
      this.checkIfFirstEntry();
      this.filterString += ('(milage:<=' + this.filters.milageTo + ')');
    }

    localStorage.setItem('filters', JSON.stringify(this.filters));
    localStorage.setItem('term', this.filterString);
    this.getFilteredCars();
  }


  stringConstructMultiSelect(feature, featureName) {
    let check = false;
    let string = '';
    // tslint:disable-next-line:forin
    for (const t in feature) {
      if (feature[t].selected === true) {
        if (check === false) {
          this.checkIfFirstEntry();
        }
        if (check) {
          string += ' OR ';
        }
        string += ('(' + featureName + ': ' + feature[t].name + ')');
        check = true;
      }
    }
    if (string.length > 0) {
      this.filterString += ('(' + string + ')');
    }
  }

  clearFilters() {
    // tslint:disable-next-line:forin
    for (const i in this.filters.manufacturers) {
      this.filters.manufacturers[i].selected = false;
      // tslint:disable-next-line:forin
      for (const j in this.filters.manufacturers[i].models) {
        this.filters.manufacturers[i].models[j].selected = false;
      }
    }
    this.clearFeature(this.filters.colors);
    this.clearFeature(this.filters.doors);
    this.clearFeature(this.filters.extraFeatures);
    this.clearFeature(this.filters.fuelTypes);
    this.clearFeature(this.filters.seating);
    this.clearFeature(this.filters.transmissions);
    // TODO! add clear selected to all other filters to come
    this.setDefaultFilters();
    localStorage.setItem('term', '');
    localStorage.setItem('filters', JSON.stringify(this.filters));
    this.getFilteredCars();
  }

  clearFeature(feature) {
    // tslint:disable-next-line:forin
    for (const i in feature) {
      feature[i].selected = false;
    }
  }

  // Helper function to construct filter string
  checkIfFirstEntry() {
    if (this.filterString.length > 0) {
      this.filterString += ' AND ';
    }
  }

  // Setting filter parameter functions
  setPriceFrom(param) {
    this.filters.priceFrom = param;
    this.constructFilterString();
  }

  setPriceTo(param) {
    this.filters.priceTo = param;
    this.constructFilterString();
  }
  setYearFrom(param) {
    this.filters.yearFrom = param;
    this.constructFilterString();
  }
  setYearTo(param) {
    this.filters.yearTo = param;
    this.constructFilterString();
  }

  setMilageFrom(param) {
    this.filters.milageFrom = param;
    this.constructFilterString();
  }

  setMilageTo(param) {
    this.filters.milageTo = param;
    this.constructFilterString();
  }
}
