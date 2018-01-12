using Model;
using Model.Common;
using Repository.Commons.Patterns;
using Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Commons.Models
{
    public interface IVehicleMakeRepository : IGenericRepository<IVehicleMakeModel>
    {
        Task<IEnumerable<IVehicleMakeModel>> GetAllAsync(ISystemDataModel systemDataModel);
    }
}
