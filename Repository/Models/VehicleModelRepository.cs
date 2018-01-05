using System.Collections.Generic;
using DAL;
using Model;
using Model.Common;
using Repository.Commons.Models;
using Repository.Patterns;

namespace Repository
{
    public class VehicleModelRepository : GenericRepository<VehicleModelModel>, IVehicleModelRepository
    {
        public VehicleModelRepository(VehicleDBContext dbContext)
            : base(dbContext)
        {

        }

        public IEnumerable<VehicleMakeModel> GetAllMakes()
        {
            throw new System.NotImplementedException();
        }
    }
}