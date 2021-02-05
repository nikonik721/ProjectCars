using ProjectCars.Models.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCars.DL.Interface
{
    public interface IRVRepository
    {
        Task<RV> Create(RV rv);
        Task<RV> Update(RV rv);
        Task<RV> GetById(int RVid);
        Task Delete(int RVid);
        Task<IEnumerable<RV>> GetAll();
    }
}
