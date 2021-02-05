using ProjectCars.BL.Interface;
using ProjectCars.DL.Interface;
using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.BL.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle> Create(Vehicle vehicle)
        {
            return await _vehicleRepository.Create(vehicle);
        }

        public async Task<Vehicle> Update(Vehicle vehicle)
        {
            return await _vehicleRepository.Update(vehicle);
        }

        public async Task Delete(int VehicleId)
        {
            await _vehicleRepository.Delete(VehicleId);
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await _vehicleRepository.GetAll();
        }

    }
}
