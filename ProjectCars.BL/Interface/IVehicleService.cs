using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.BL.Interface
{
    public interface IVehicleService
    {
        Task<Vehicle> Create(Vehicle vehicle);
        Task<Vehicle> Update(Vehicle vehicle);
        Task Delete(int Vehicleid);
        Task<IEnumerable<Vehicle>> GetAll();
    }
}
