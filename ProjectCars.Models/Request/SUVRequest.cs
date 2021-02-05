using System;

namespace ProjectCars.Models.Vehicle
{
    public class SUVRequest
    {
        public int SUVId { get; set; }
        public string SUVBrand { get; set; }
        public string SUVModel { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public string SUVType { get; set; }
        public string Engine { get; set; }
        public string Fuel { get; set; }
    }
}
