using DAL;
using Model;
using Repository.Commons.Models;
using Repository.Patterns;

namespace Repository
{
    public class VehicleMakeRepository : GenericRepository<VehicleMakeModel>, IVehicleMakeRepository
    {
        public VehicleMakeRepository(VehicleDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}