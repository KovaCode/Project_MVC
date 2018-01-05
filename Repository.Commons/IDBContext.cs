using Model;
using Model.Common;
using System.Data.Entity;


namespace Repository.Commons
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : VehicleBase;
        int SaveChanges();
    }
}
