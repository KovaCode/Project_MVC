using Model;
using System.Collections.Generic;

namespace Repository.Commons
{
    public interface IModelRepository : IGenericRepository<VehicleModel>
    {
        IEnumerable<VehicleMake> GetAllMakes();
    }
}
