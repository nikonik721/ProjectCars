using System;

namespace ProjectCars.Models.Vehicle
{
    public class Vehicle
    {
        public int VehicleId { set; get; }
        public string VehicleBrand { set; get; }
        public string VehicleModel { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public string VehicleColor { set; get; }
        public string VehicleType { get; set; }
        public string Engine { get; set; }
        public string Fuel { get; set; }
    }

}
