using System.Collections.Generic;
using DAL;
using Model;
using Repository.Commons;

namespace Repository
{
    public class ModelRepository : GenericRepository<Model.VehicleModel>, IModelRepository
    {
        public ModelRepository(VehicleDBContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<VehicleMake> GetAllMakes()
        {
            throw new System.NotImplementedException();
        }
    }
}