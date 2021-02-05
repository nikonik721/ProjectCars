using ProjectCars.BL.Interface;
using ProjectCars.DL.Interface;
using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.BL.Services
{
    public class SUVService : ISUVService
    {
        private readonly ISUVRepository _suvRepository;

        public SUVService(ISUVRepository suvRepository)
        {
            _suvRepository = suvRepository;
        }

        public async Task<SUV> Create(SUV suv)
        {
            return await _suvRepository.Create(suv);
        }

        public async Task<SUV> Update(SUV suv)
        {
            return await _suvRepository.Update(suv);
        }

        public async Task Delete(int SUVid)
        {
            await _suvRepository.Delete(SUVid);
        }

        public async Task<IEnumerable<SUV>> GetAll()
        {
            return await _suvRepository.GetAll();
        }

    }
}
