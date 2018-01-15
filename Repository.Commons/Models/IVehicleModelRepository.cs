using Model;
using Model.Common;
using Repository.Commons.Patterns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Commons.Models
{
    public interface IVehicleModelRepository : IGenericRepository<IVehicleModelModel>
    {
        Task<IEnumerable<IVehicleMakeModel>> GetAllMakesAsync();
    }
}
