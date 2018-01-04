using DAL;
using Model.Common;
using Repository.Commons;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly VehicleDBContext vehicleDBContext;
        protected UnitOfWork unitOfWork;

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


        public async Task<TEntity> GetByIdAsync(Guid? id)
        {
            return await vehicleDBContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await unitOfWork.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var ent = await GetByIdAsync(id);
            await unitOfWork.UpdateAsync(ent);
        }

        public async Task DeleteAsync(Guid? id)
        {
            var ent = await GetByIdAsync(id);
            await unitOfWork.DeleteAsync(ent);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await unitOfWork.DeleteAsync(entity);
        }
    }
}
