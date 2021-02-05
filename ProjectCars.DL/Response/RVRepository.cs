using ProjectCars.DL.InMemoryDB;
using ProjectCars.DL.Interface;
using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCars.DL.Services
{
    public class RVRepository : IRVRepository
    {
        private static List<RV> DBTable;

        public RVRepository()
        {
            DBTable = InMemoryDb.RVs;
        }

        public Task<RV> Create(RV rv)
        {
            DBTable.Add(rv);
            return Task.FromResult(rv);
        }

        public Task Delete(int RVid)
        {
            var result = DBTable.FirstOrDefault(x => x.RVid == RVid);

            if (result != null)
            {
                DBTable.Remove(result);
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<RV>> GetAll()
        {
            return await Task.FromResult(DBTable);
        }

        public async Task<RV> GetById(int RVId)
        {
            return await Task.FromResult(DBTable.FirstOrDefault(x => x.RVid == RVId));
        }

        public async Task<RV> Update(RV rv)
        {
            var result = DBTable.FirstOrDefault(x => x.RVid == rv.RVid);

            if (result != null)
            {
                await Delete(rv.RVid);
                return await Create(rv);
            }

            return null;
        }
    }
}
