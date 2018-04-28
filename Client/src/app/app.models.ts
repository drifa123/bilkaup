/**
 *    DTO MODELS
 **/

// This DTO needs to be changed, more to add
export class CarDTO {
    serialNum: number;
    manufacturer: string;
    model: string;
    modelType: string;
    imgLink: string;
    price: number;
    offerPrice: number;
    milage: number;
    transmission: string;
    onSite: boolean;
    year: string;
}

export class CarCardDTO {
    // TODO!!!!!!!!
    serialNumber: number;
    manufacturer: string;
    model: string;
    price: number;
    offerPrice: number;
    modelType: string;
    milage: number;
    seating: number;
    fueltypes: string[];
    year: number;
}

export class FuelTypeDTO {
    id: number;
    fuel: string;
}

export class DriveSteeringDTO {
    id: number;
    name: string;
}

export class WheelDTO {
    id: number;
    name: string;
}

export class CarDetailDTO {
    id: number;
    manufacturer: string;
    model: string;
    modelType: string;
    price: number;
    co2: number;
    weight: number;
    nextCheckUp: string;
    milage: number;
    fuelType: string;
    wheel: string;
    cylinders: number;
    cc: number;
    injection: boolean;
    horsepower: number;
    transmission: string;
    drive: number;
    driveSteering: string[];
    seating: number;
    doors: number;
    moreInfo: string;
    dateSale: string;
    dateUpdate: string;
    onSite: boolean;
    color: string;
    carsaleId: number;
    carsaleName: string;
    carsalePhoneNum: string;
    carsaleAddress: string;
    carsaleWebsite: string;
}

export class CarSaleBasicDTO {
    id: number;
    name: string;
    email: string;
    phoneNum: string;
    address: string;
    website: string;
}

export class FilterDTO {
    priceFrom: number;
    priceTo: number;
    milageFrom: number;
    milageTo: number;
    yearFrom: number;
    yearTo: number;
    manufacturers: ManufacturerDTO[];
    prices: number[];
    milages: number[];
    years: number[];
    transmissions: CarFeatureDTO[];
    extraFeatures: CarFeatureDTO[];
    doors: CarFeatureDTO[];
    fuelTypes: CarFeatureDTO[];
    colors: CarFeatureDTO[];
    seating: CarFeatureDTO[];
}

export class CarFeatureDTO {
    name: string;
    selected: boolean;
}

export class ManufacturerDTO {
    name: string;
    models: CarFeatureDTO[];
    selected: boolean;
}

export class CarSaleDetailDTO {
    id: number;
    name: string;
    ssn: string;
    email: string;
    phoneNum: string;
    address: string;
    webpage: string;
    openingHours: OpeningHoursDTO;
    cars: CarDTO[];
}

export class OpeningHoursDTO {
    monday: string;
    tuesday: string;
    wednesday: string;
    thursday: string;
    friday: string;
    saturday: string;
    sunday: string;
    other: string;
}

export class CarSaleDTO {
    id: number;
    name: string;
    ssn: string;
    email: string;
    phoneNum: string;
    address: string;
    webpage: string;
    accepted: boolean;
    active: boolean;
}


export class LoginDTO {
    id: number;
    role: string;
    token: string;
}

export class SamgongustofaDTO {
    fastanumer: string;
    skraningarnumer: string;
    verksmidjunumer: string;
    tegund: string;
    undirtegund: string;
    litur: string;
    firstskrad: string;
    stada: string;
    naestaadalskodun: string;
    co2losun: number;
    eiginthyngd: number;
}

export class UserInfoDTO {
    email: string;
    id: number;
}

/**
 *    VIEW MODELS
 **/
export class CarSaleViewModel {
    name: string;
    ssn: string;
    email: string;
    phoneNum: string;
    address: string;
    webpage: string;
}

// Used when admin registers a new user
export class RegisterViewModel {
    email: string;
    role: string;
}

export class CarViewModel {
    manufacturer: string;
    model: string;
    regNum: string;
    year: string;
    color: string;
    co2: number;
    weight: number;
    status: string;
    nextCheckUp: string;
    carSaleId: number;
    price: number;
    driven: number;
    doors: number;
    seating: number;
    cylinders: number;
    horsepower: number;
    injection: boolean;
    cc: number;
    onSite: boolean;
    wheel: number[];
    fuelType: number[];
    drive: number;
    transmission: number;
    driveSteering: number[];
}

// Used when user is logging in
export class LoginViewModel {
    email: string;
    password: string;
    rememberMe: boolean;
}

