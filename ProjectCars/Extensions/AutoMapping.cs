using AutoMapper;
using ProjectCars.Models.Vehicle;
using System.Collections.Generic;

namespace ProjectCars.Extensions
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RVResponse, RV>().ReverseMap();
            CreateMap<SUVResponse, SUV>().ReverseMap();
            CreateMap<VehicleResponse, Vehicle>().ReverseMap();

            CreateMap<RVRequest, RV>().ReverseMap();
            CreateMap<SUVRequest, SUV>().ReverseMap();
            CreateMap<VehicleRequest, Vehicle>().ReverseMap();

            CreateMap<IEnumerable<VehicleResponse>, IEnumerable<Vehicle>>();
            CreateMap<IEnumerable<SUVResponse>, IEnumerable<SUV>>();
            CreateMap<IEnumerable<RVResponse>, IEnumerable<RV>>();
        }
    }
}
