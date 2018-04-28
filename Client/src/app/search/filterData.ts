import { Injectable } from '@angular/core';
import { FilterDTO } from '../app.models';

export const FilterData = {
    prices: [300000, 400000, 500000, 600000, 700000, 800000, 900000, 1000000, 1100000, 1200000,
        1300000, 1400000, 1500000, 1600000, 1700000, 1800000, 1900000, 2000000, 3000000, 4000000, 5000000,
        6000000, 7000000, 8000000, 9000000, 10000000, 11000000, 12000000, 13000000, 14000000, 15000000],
    milages: [10000, 20000, 30000, 40000, 50000, 60000, 70000, 80000, 90000, 100000, 110000,
        120000, 130000, 140000, 150000, 160000, 170000, 180000, 190000, 200000, 250000, 300000, 350000, 400000, 450000, 500000],
    transmissions: [{name: 'Beinskipting', selected: false}, {name: 'Sjálfskipting', selected: false}],
    extraFeatures: [{name: 'Leðursæti', selected: false}, {name: 'Bluetooth', selected: false},
    {name: 'Rafdrifnar rúður', selected: false}, {name: 'Hiti í framsætum', selected: false}],
    doors: [{name: '2', selected: false}, {name: '3', selected: false}, {name: '4', selected: false},
    {name: '5', selected: false}, {name: '6', selected: false}, {name: '7', selected: false}],
    fuelTypes: [{name: 'Bensín', selected: false}, {name: 'Dísel', selected: false},
    {name: 'Metan', selected: false}, {name: 'Rafmagn', selected: false} ],
    colors: [{name: 'Gulur', selected: false}, {name: 'Rauður', selected: false}, {name: 'Grænn', selected: false},
    {name: 'Blár', selected: false}, {name: 'Svartur', selected: false}, {name: 'Hvítur', selected: false},
    {name: 'Fjólublár', selected: false}, {name: 'Brúnn', selected: false},
    {name: 'Bleikur', selected: false}, {name: 'Appelsínugulur', selected: false}, {name: 'Grár', selected: false},
    {name: 'Gylltur', selected: false}],
    seating: [{name: '1', selected: false}, {name: '2', selected: false}, {name: '3', selected: false}, {name: '4', selected: false},
    {name: '5', selected: false}, {name: '6', selected: false}, {name: '7', selected: false}, {name: '8', selected: false},
    {name: '9', selected: false}, {name: '9', selected: false}, {name: '10', selected: false}, {name: '11', selected: false},
    {name: '12', selected: false}, {name: '13', selected: false}, {name: '14', selected: false}, {name: '15', selected: false},
    {name: '16', selected: false}, {name: '17', selected: false}, {name: '18', selected: false}]
};
export const sortFilters = [
    {name: 'Nýustu bílar', value: 'serialNumber'},
    {name: 'Akstur', value: 'milage'},
    {name: 'Verð', value: 'price'},
   {name: 'Árgerð', value: 'year'}
];

