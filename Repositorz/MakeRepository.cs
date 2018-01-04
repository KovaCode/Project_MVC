using System.Threading.Tasks;
using DAL;
using Model;
using Repository.Commons;

namespace Repository
{
    public class MakeRepository : GenericRepository<Make>, IMakeRepository
    {
        public MakeRepository(VehicleDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}