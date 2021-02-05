using ProjectCars.BL.Interface;
using ProjectCars.DL.Interface;
using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.BL.Services
{
    public class RVService : IRVService
    {
        private readonly IRVRepository _rvRepository;

        public RVService(IRVRepository rvRepository)
        {
            _rvRepository = rvRepository;
        }

        public async Task<RV> Create(RV rv)
        {
            return await _rvRepository.Create(rv);
        }

        public async Task<RV> Update(RV rv)
        {
            return await _rvRepository.Update(rv);
        }

        public async Task Delete(int RVId)
        {
            await _rvRepository.Delete(RVId);
        }

        public async Task<IEnumerable<RV>> GetAll()
        {
            return await _rvRepository.GetAll();
        }

    }
}
