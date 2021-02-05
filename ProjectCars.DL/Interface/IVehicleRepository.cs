using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.DL.Interface
{
    public interface IVehicleRepository
    {
        Task<Vehicle> Create(Vehicle vehicle);
        Task<Vehicle> Update(Vehicle vehicle);
        Task<Vehicle> GetById(int VehicleId);
        Task Delete(int VehicleId);
        Task<IEnumerable<Vehicle>> GetAll();

    }
}
