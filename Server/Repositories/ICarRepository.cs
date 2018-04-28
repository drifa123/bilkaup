using System.Collections.Generic;
using Bilkaup.Models.DTOModels;
using Bilkaup.Models.EntityModels;
using Bilkaup.Models.ViewModels;

namespace Bilkaup.Repositories
{
    /// <summary>
    /// Connects everything that has to do with cars to the database.
    /// </summary>
    public interface ICarRepository
    {
        /// <summary>
        /// Gets all unsold cars from the database.
        /// </summary>
        IEnumerable<CarCardDTO> GetCars();

        IEnumerable<WheelDTO> GetWheels();

        IEnumerable<FuelTypeDTO> GetFuelTypes();
        IEnumerable<DriveSteeringDTO> GetDriveSteeringInfos();

        int AddCar(Car car);

        int AddManufacturer(Manufacturer manufacturer);

        int GetManufacturerIdByName(string manufacturer);

        int GetModelIdByName(int manufacturerId, string model);

        int AddModel(Model model);

        CarDetailDTO GetCarDetail(int carID, int serialNum);

        bool AddSellerInfo(SaleInfo newInfo);
        
        FilterDTO GetFilters();

        int GetCarIDBySerial(int serialNum);

        /// <summary>
        /// Marks car as sold with date
        /// </summary>
        IEnumerable<CarCardDTO> SellCar(SaleInfo sold);
        
        bool AddWheelCar(WheelCar wc);

        bool AddFuelTypeCar(FuelTypeCar fc);

        bool AddDriveSteeringInfoCar(DriveSteeringInfoCar dc);
        
    }
}