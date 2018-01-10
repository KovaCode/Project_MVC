using DAL;
using Model.Common;
using Repository.Commons.Patterns;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Patterns
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IVehicleBase
    {
        protected UnitOfWork unitOfWork;
        private readonly VehicleDBContext vehicleDBContext;

        public GenericRepository(VehicleDBContext dbContext)
        {
            vehicleDBContext = dbContext;
            unitOfWork = new UnitOfWork(vehicleDBContext);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return vehicleDBContext.Set<TEntity>().AsNoTracking().AsEnumerable();
        }

        public IQueryable<TEntity> GetAllQueryable()
        {
            return vehicleDBContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(Guid? id)
        {
            return await vehicleDBContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await unitOfWork.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await unitOfWork.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid? id)
        {
            var ent = await GetById(id);
            await unitOfWork.DeleteAsync(ent);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await unitOfWork.DeleteAsync(entity);
        }

    }
}
