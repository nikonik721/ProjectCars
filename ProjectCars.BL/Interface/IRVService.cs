using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.BL.Interface
{
    public interface IRVService
    {
        Task<RV> Create(RV rv);
        Task<RV> Update(RV rv);
        Task Delete(int RVid);
        Task<IEnumerable<RV>> GetAll();
    }
}
