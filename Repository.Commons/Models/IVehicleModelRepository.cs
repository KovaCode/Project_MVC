using Model;
using Model.Common;
using Repository.Commons.Patterns;
using System.Collections.Generic;

namespace Repository.Commons.Models
{
    public interface IVehicleModelRepository : IGenericRepository<VehicleModelModel>
    {
        IEnumerable<VehicleMakeModel> GetAllMakes();
    }
}
