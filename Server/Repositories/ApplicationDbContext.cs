using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bilkaup.Models;
using Bilkaup.Models.EntityModels;

namespace Bilkaup.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<CarSale>()
                .HasKey(key => key.ID);
                
            builder.Entity<Car>()
                .HasKey(key => key.ID);
            
            builder.Entity<Manufacturer>()
                .HasKey(key => key.ID);
            
            builder.Entity<Model>()
                .HasKey(key => key.ID);

            builder.Entity<Drive>()
                .HasKey(key => key.ID);
            
            builder.Entity<ModelType>()
                .HasKey(key => key.ID);

            builder.Entity<Transmission>()
                .HasKey(key => key.ID);
            
            builder.Entity<Picture>()
                .HasKey(key => new {key.CarSerialNum, key.Link});
            
            builder.Entity<PassengerSpaceCar>()
                .HasKey(key => new {key.PassengerSpaceID, key.CarID});
            
            builder.Entity<PassengerSpace>()
                .HasKey(key => key.ID);
            
            builder.Entity<ExtraFeaturesCar>()
                .HasKey(key => new {key.CarID, key.ExtraFeaturesID});
            
            builder.Entity<ExtraFeature>()
                .HasKey(key => key.ID);
            
            builder.Entity<DriveSteeringInfoCar>()
                .HasKey(key => new {key.CarID, key.DriveSteeringID});
            
            builder.Entity<DriveSteeringInfo>()
                .HasKey(key => key.ID);
            
            builder.Entity<SellerCar>()
                .HasKey(key => new {key.CarID, key.SellerID});

            builder.Entity<Seller>()
                .HasKey(key => key.ID);

            builder.Entity<SaleInfo>()
                .HasKey(key => new {key.CarID, key.SerialNum});

            builder.Entity<CarSaleOpening>()
                .HasKey(key => key.CarSaleID);

            builder.Entity<FuelTypeCar>()
                .HasKey(key => new {key.CarID, key.FuelTypeID});
                
            builder.Entity<FuelType>()
                .HasKey(key => key.ID);

            builder.Entity<Wheel>()
                .HasKey(key => key.ID);
            
            builder.Entity<WheelCar>()
                .HasKey(key => new {key.CarID, key.WheelID});
        }

        public DbSet<CarSale> CarSales { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Drive> Drives { get; set; }
        public DbSet<ModelType> ModelTypes { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<PassengerSpaceCar> PassengerSpaceCars { get; set; }
        public DbSet<PassengerSpace> PassengerSpaces { get; set; }
        public DbSet<ExtraFeaturesCar> ExtraFeaturesCars { get; set; }
        public DbSet<ExtraFeature> ExtraFeatures { get; set; }
        public DbSet<DriveSteeringInfoCar> DriveSteeringInfoCars { get; set; }
        public DbSet<DriveSteeringInfo> DriveSteeringInfos { get; set; }
        public DbSet<SellerCar> SellerCars { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SaleInfo> SaleInfos { get; set; }
        public DbSet<CarSaleOpening> CarSaleOpenings { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<FuelTypeCar> FuelTypeCars { get; set; }
        public DbSet<Wheel> Wheels { get; set; }
        public DbSet<WheelCar> WheelCars { get; set; }
    }
}
