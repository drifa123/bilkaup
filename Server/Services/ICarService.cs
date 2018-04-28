using System;
using System.Collections.Generic;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.EntityModels;
using Bilkaup.Models.ViewModels;

namespace Bilkaup.Services
{
    public interface ICarService
    {
        IEnumerable<CarCardDTO> GetCars();

        IEnumerable<WheelDTO> GetWheels();

        IEnumerable<FuelTypeDTO> GetFuelTypes();

        IEnumerable<DriveSteeringDTO> GetDriveSteeringInfos();

        int AddCar(CarViewModel car);

        int CheckManufacturerByName(string manufacturer);

        int CheckModelByName(int manufacturerId, string model);
        
        FilterDTO GetFilters();

        CarDetailDTO GetCarBySerialNum(int id);

        IEnumerable<CarCardDTO> SellCar(int serialNum);
    }
}