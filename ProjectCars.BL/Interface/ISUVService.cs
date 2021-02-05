using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.BL.Interface
{
    public interface ISUVService
    {
        Task<SUV> Create(SUV suv);
        Task<SUV> Update(SUV suv);
        Task Delete(int SUVid);
        Task<IEnumerable<SUV>> GetAll();
    }
}
