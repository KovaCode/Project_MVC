using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model;

namespace Repository.Commons
{
    public interface IMakeRepository : IGenericRepository<VehicleMake>
    {
    }
}
