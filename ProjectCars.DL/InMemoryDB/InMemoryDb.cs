using ProjectCars.Models.Vehicle;
using System;
using System.Collections.Generic;

namespace ProjectCars.DL.InMemoryDB
{
    public static class InMemoryDb
    {
        public static List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public static List<RV> RVs { get; set; } = new List<RV>();
        public static List<SUV> SUVs { get; set; } = new List<SUV>();
        public static void Init()
        {

            Vehicles.Add(new Vehicle
            {
                VehicleId = 7,
                VehicleBrand = "Audi",
                VehicleModel = "RSQ3",
                DateOfManufacturing = new DateTime(2020, 02, 03),
                VehicleColor = "Metallic Silver",
                VehicleType = "RWD",
                Engine = "400HP Euro 6",
                Fuel = "Gasoline"

            });


            RVs.Add(new RV
            {
                RVid = 4,
                RVBrand = "Compass",
                RVModel = "RUV",
                DateOfManufacturing = new DateTime(2021, 11, 09),
                RVColor = "Champagne",
                Engine = "3.5L V6 EcoBoost® Turbo, 310HP",
                Fuel = "Gasoline"
            });


            SUVs.Add(new SUV
            {
                SUVId = 2,
                SUVBrand = "Volvo",
                SUVModel = "XC40",
                DateOfManufacturing = new DateTime(2021, 05, 30),
                SUVType = "AWD",
                Engine = "150hp D3",
                Fuel = "Diesel"
            });
        }
    }

}
