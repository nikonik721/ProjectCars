using ProjectCars.DL.InMemoryDB;
using ProjectCars.DL.Interface;
using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCars.DL.Services
{
    public class VehicleRepository : IVehicleRepository
    {
        private static List<Vehicle> DBTable;

        public VehicleRepository()
        {
            DBTable = InMemoryDb.Vehicles;
        }

        public Task<Vehicle> Create(Vehicle vehicle)
        {
            DBTable.Add(vehicle);
            return Task.FromResult(vehicle);
        }

        public async Task<Vehicle> Update(Vehicle vehicle)
        {
            var result = DBTable.FirstOrDefault(x => x.VehicleId == vehicle.VehicleId);

            if (result != null)
            {
                await Delete(vehicle.VehicleId);
                return await Create(vehicle);
            }

            return null;
        }

        public Task Delete(int VehicleId)
        {
            var result = DBTable.FirstOrDefault(x => x.VehicleId == VehicleId);

            if (result != null)
            {
                DBTable.Remove(result);
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await Task.FromResult(DBTable);
        }

        public async Task<Vehicle> GetById(int VehicleId)
        {
            return await Task.FromResult(DBTable.FirstOrDefault(x => x.VehicleId == VehicleId));
        }
    }
}
