using DAL;
using Model.Common;
using Repository.Commons.Patterns;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Service.Common;

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

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(vehicleDBContext.Set<TEntity>().AsEnumerable());
        }

        public async Task<IQueryable<TEntity>> GetAllQueryableAsync()
        {
            return await Task.FromResult(vehicleDBContext.Set<TEntity>().AsQueryable<TEntity>());
        }

        public async Task<TEntity> ReadAsync(Guid? id)
        {
            return await vehicleDBContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            await unitOfWork.AddAsync(entity);
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            await unitOfWork.UpdateAsync(entity);
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> DeleteAsync(Guid? id)
        {
            var ent = await ReadAsync(id);
            await unitOfWork.DeleteAsync(ent);
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            await unitOfWork.DeleteAsync(entity);
            return await unitOfWork.CommitAsync();
        }
    }
}
