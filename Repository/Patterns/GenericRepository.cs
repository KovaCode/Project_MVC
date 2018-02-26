using DAL;
using Model.Common;
using Repository.Commons.Patterns;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Service.Common;
using AutoMapper;
using PagedList;
using Common;

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
            IEnumerable<TEntity> listItems =  await vehicleDBContext.Set<TEntity>().ToListAsync();
            return listItems;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISystemDataModel systemDataModel)
        {
            IQueryable<TEntity> items = await GetAllQueryableAsync();

            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                items = items.Where(s => s.Name.Contains(systemDataModel.SearchValue));
            }

            if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
            {
                items = items.OrderByDescending(s => s.Name);
            }
            else
            {
                items = items.OrderBy(s => s.Name);
            }

            IEnumerable<TEntity> enumItems = Mapper.Map<IEnumerable<TEntity>>(items.AsEnumerable());

            return enumItems;
        }

        public async Task<StaticPagedList<TEntity>> GetAllPagedAsync(ISystemDataModel systemDataModel)
        {
            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                systemDataModel.Page = 1;
            }
            else
            {
                systemDataModel.SearchValue = systemDataModel.CurrentFilter;
            }

            IQueryable<TEntity> items = await GetAllQueryableAsync();

            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                items = items.Where(s => s.Name.Contains(systemDataModel.SearchValue, StringComparison.OrdinalIgnoreCase));
            }

            if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
            {
                items = items.OrderByDescending(s => s.Name);
            }
            else
            {
                items = items.OrderBy(s => s.Name);
            }
            
            systemDataModel.TotalCount = items.Count();

            items = items.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            return new StaticPagedList<TEntity>(items, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);
           
        }

        public async Task<IQueryable<TEntity>> GetAllQueryableAsync()
        {
            IEnumerable<TEntity> items = await GetAllAsync();
            return items.AsQueryable();
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

        public async Task<int> UpdateAsync(TEntity entityUpdate)
        {
            //TEntity entity = await ReadAsync(id);

            await unitOfWork.UpdateAsync(entityUpdate);
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
