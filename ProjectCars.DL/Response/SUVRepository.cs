using ProjectCars.DL.InMemoryDB;
using ProjectCars.DL.Interface;
using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCars.DL.Services
{
    public class SUVRepository : ISUVRepository
    {
        private static List<SUV> DBTable;

        public SUVRepository()
        {
            DBTable = InMemoryDb.SUVs;
        }

        public Task<SUV> Create(SUV suv)
        {
            DBTable.Add(suv);
            return Task.FromResult(suv);
        }

        public async Task<SUV> Update(SUV suv)
        {
            var result = DBTable.FirstOrDefault(x => x.SUVId == suv.SUVId);

            if (result != null)
            {
                await Delete(suv.SUVId);
                return await Create(suv);
            }

            return null;
        }

        public Task Delete(int SUVId)
        {
            var result = DBTable.FirstOrDefault(x => x.SUVId == SUVId);

            if (result != null)
            {
                DBTable.Remove(result);
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<SUV>> GetAll()
        {
            return await Task.FromResult(DBTable);
        }

        public async Task<SUV> GetById(int SUVId)
        {
            return await Task.FromResult(DBTable.FirstOrDefault(x => x.SUVId == SUVId));
        }
    }
}
