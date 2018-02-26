using Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Common.Services
{
    public interface IVehicleModelService : IVehicleService<IVehicleModelModel>
    {
        Task<IEnumerable<IVehicleMakeModel>> GetAllMakeAsync();
    }
}
