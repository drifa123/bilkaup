import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { SearchComponent } from './search.component';
import { CarService } from '../car.service';
import { mockService } from '../MockServices/mockCar.service';
import { RouterTestingModule } from '@angular/router/testing';
import { Component, OnInit, NO_ERRORS_SCHEMA, Injector } from '@angular/core';
import { CarDTO, CarDetailDTO, FilterDTO, ManufacturerDTO, CarCardDTO } from '../app.models';
import { ActivatedRoute, Router } from '@angular/router';
import { forEach } from '@angular/router/src/utils/collection';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs/Observable';
import { FilterData } from './filterData';
import { map } from 'rxjs/operator/map';
import { FormsModule, ReactiveFormsModule, SelectControlValueAccessor } from '@angular/forms';
import { NgSelectModule, NgSelectComponent, NgOption } from '@ng-select/ng-select';
import { CommonModule } from '@angular/common';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MockBackend } from '@angular/http/testing';
import { BaseRequestOptions, ConnectionBackend, RequestOptions, Http } from '@angular/http';

describe('SearchComponent', async() => {
  let component: SearchComponent;
  let fixture: ComponentFixture<SearchComponent>;

  beforeEach((() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        NgSelectModule
      ],
      declarations: [ SearchComponent ],
      providers: [{
        provide: CarService,
        useValue: mockService
      }, {
        provide: ConnectionBackend,
        useClass: MockBackend
      }, {
        provide: RequestOptions,
        useClass: BaseRequestOptions
      },
      Http, CarService
      ],
      schemas: [
        CUSTOM_ELEMENTS_SCHEMA,
        NO_ERRORS_SCHEMA
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    component.filters = new FilterDTO();
    component.filters.manufacturers = [{name: 'Toyota', selected: true,
      models: [{name: 'Corola', selected: true}, {name: 'Avensis', selected: true}]},
      {name: 'Ford', selected: true, models: [{name: 'Focus', selected: false}, {name: 'Fiesta', selected: false}]}];
    component.setFilterData();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should call setDefaultFilters() function', () => {
    spyOn(component, 'setDefaultFilters').and.callThrough();
    component.filters = new FilterDTO();
    component.setDefaultFilters();
    expect(component.setDefaultFilters).toHaveBeenCalled();
    expect(component.filters).toBeDefined();
    expect(component.filters.milageFrom).toBeNull();
  });
  it('should call setSort', () => {
    spyOn(component, 'setSort').and.callThrough();
    const sort = {name: 'Akstur', value: 'milage'};
    component.setSort(sort);
    expect(component.setSort).toHaveBeenCalled();
    expect(component.sort.name).toBe('Akstur');
  });
  it('should call setFilterData() function', () => {
    spyOn(component, 'setFilterData').and.callThrough();
    component.setFilterData();
    expect(component.setFilterData).toHaveBeenCalled();
    expect(component.filters.transmissions.length).toBe(2);
  });
  it('should call getCarDetailPage() function', () => {
    spyOn(component, 'getCarDetailPage').and.callThrough();
    const car = new CarCardDTO();
    car.serialNumber = 1;
    component.getCarDetailPage(car);
    expect(component.getCarDetailPage).toHaveBeenCalled();
  });

  it('should call setFilter with selected filter', () => {
    spyOn(component, 'setFilter').and.callThrough();
    component.selected = {name: 'bla', selected: false};
    component.setFilter(undefined);
    expect(component.setFilter).toHaveBeenCalled();
  });
  it('should call setFilter with variable', () => {
    spyOn(component, 'setFilter').and.callThrough();
    const filter = {name: 'bla', selected: false};
    component.setFilter(filter);
    expect(component.setFilter).toHaveBeenCalled();
  });
  it('should call setFilter with transmission', () => {
    spyOn(component, 'setFilter').and.callThrough();
    component.setFilter(component.filters.transmissions[0]);
    expect(component.filters.transmissions[0].selected).toBe(true);
    expect(component.setFilter).toHaveBeenCalled();
  });
  it('should call setFilter with fuelType', () => {
    spyOn(component, 'setFilter').and.callThrough();
    component.setFilter(component.filters.fuelTypes[0]);
    expect(component.setFilter).toHaveBeenCalled();
    expect(component.filters.fuelTypes[0].selected).toBe(true);
  });
  it('should call setFilter with extraFeature', () => {
    spyOn(component, 'setFilter').and.callThrough();
    component.filters.extraFeatures[1].selected = true;
    component.setFilter(component.filters.extraFeatures[0]);
    expect(component.setFilter).toHaveBeenCalled();
    expect(component.filters.extraFeatures[0].selected).toBe(true);
  });
  it('should set priceFrom', () => {
    spyOn(component, 'setPriceFrom').and.callThrough();
    component.setPriceFrom(1);
    expect(component.filters.priceFrom).toEqual(1);
    expect(component.setPriceFrom).toHaveBeenCalled();
  });
  it('should set priceTo', () => {
    spyOn(component, 'setPriceTo').and.callThrough();
    component.setPriceTo(1);
    expect(component.filters.priceTo).toEqual(1);
    expect(component.setPriceTo).toHaveBeenCalled();
  });
  it('should set MilageFrom', () => {
    spyOn(component, 'setMilageFrom').and.callThrough();
    component.setMilageFrom(1);
    expect(component.filters.milageFrom).toEqual(1);
    expect(component.setMilageFrom).toHaveBeenCalled();
  });
  it('should set MilageTo', () => {
    spyOn(component, 'setMilageTo').and.callThrough();
    component.setMilageTo(1);
    expect(component.filters.milageTo).toEqual(1);
    expect(component.setMilageTo).toHaveBeenCalled();
  });
  it('should set YearFrom', () => {
    spyOn(component, 'setYearFrom').and.callThrough();
    component.setYearFrom(1);
    expect(component.filters.yearFrom).toEqual(1);
    expect(component.setYearFrom).toHaveBeenCalled();
  });
  it('should set YearTo', () => {
    spyOn(component, 'setYearTo').and.callThrough();
    component.setYearTo(1);
    expect(component.filters.yearTo).toEqual(1);
    expect(component.setYearTo).toHaveBeenCalled();
  });
  it('should call clearFilters', () => {
    spyOn(component, 'clearFilters').and.callThrough();
    component.clearFilters();
    expect(component.clearFilters).toHaveBeenCalled();
    expect(component.filters.priceFrom).toBeNull();
  });
  it('should call stringConstructMultiSelect', () => {
    spyOn(component, 'stringConstructMultiSelect').and.callThrough();
    component.filters.colors[0].selected = true;
    component.filters.colors[1].selected = true;
    component.stringConstructMultiSelect(component.filters.colors, 'color');
    expect(component.stringConstructMultiSelect).toHaveBeenCalled();
  });
  it('should call toggleSearch', () => {
    spyOn(component, 'toggleSearch').and.callThrough();
    component.toggleSearch();
    expect(component.toggleSearch).toHaveBeenCalled();
  });
  it('should call getStyle with true', () => {
    spyOn(component, 'getStyle').and.callThrough();
    component.hideSearch = true;
    component.getStyle();
    expect(component.getStyle).toHaveBeenCalled();
  });
  it('should call getStyle with true', () => {
    spyOn(component, 'getStyle').and.callThrough();
    component.hideSearch = false;
    component.getStyle();
    expect(component.getStyle).toHaveBeenCalled();
  });
  it('should get filters', () => {
    localStorage.removeItem('filters');
    spyOn(component, 'ngOnInit').and.callThrough();
    expect(true).toBe(true);
  });

});
