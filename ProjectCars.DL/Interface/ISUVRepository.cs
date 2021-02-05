using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.DL.Interface
{
    public interface ISUVRepository
    {
        Task<SUV> Create(SUV suv);
        Task<SUV> Update(SUV suv);
        Task<SUV> GetById(int SUVId);
        Task Delete(int SUVId);
        Task<IEnumerable<SUV>> GetAll();

    }
}
