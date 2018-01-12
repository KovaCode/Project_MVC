using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Model.Common;
using Repository.Commons.Models;
using Repository.Patterns;
using Repository.Commons.Patterns;
using Model;
using DAL.Entity;
using AutoMapper;
using Service.Common;

namespace Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        IGenericRepository<VehicleMakeEntity> repository;

        public VehicleMakeRepository(VehicleDBContext context)
        {
            repository = new GenericRepository<VehicleMakeEntity>(context);
        }

        public async Task<int> CreateAsync(IVehicleMakeModel entity)
        {
           return await repository.CreateAsync(Mapper.Map<VehicleMakeEntity>(entity));
            
        }

        public async Task<IVehicleMakeModel> ReadAsync(Guid? id)
        {
            return await Task.FromResult(Mapper.Map<IVehicleMakeModel>(repository.ReadAsync(id)));
        }

        public async Task<int> UpdateAsync(IVehicleMakeModel entity)
        {
            return await repository.UpdateAsync(Mapper.Map<VehicleMakeEntity>(entity));
        }

        public async Task<int> DeleteAsync(Guid? id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<int> DeleteAsync(IVehicleMakeModel entity)
        {
            return await repository.DeleteAsync(Mapper.Map<VehicleMakeEntity>(entity));
        }

        public async Task<IEnumerable<IVehicleMakeModel>> GetAllAsync()
        {
            return await Task.FromResult(Mapper.Map<IEnumerable<IVehicleMakeModel>>(repository.GetAllAsync()));
        }
   
        public async Task<IEnumerable<IVehicleMakeModel>> GetAllAsync(ISystemDataModel systemDataModel)
        {
            IQueryable<VehicleMakeEntity> makeItems = await repository.GetAllQueryableAsync();

            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                makeItems = makeItems.Where(s => s.Name.Contains(systemDataModel.SearchValue));
            }

            if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
            {
                makeItems = makeItems.OrderByDescending(s => s.Name);
            }
            else
            {
                makeItems = makeItems.OrderBy(s => s.Name);
            }
            return await Task.FromResult(Mapper.Map<IEnumerable<IVehicleMakeModel>>(makeItems.AsEnumerable()));
        }
        
        public async Task<IQueryable<IVehicleMakeModel>> GetAllQueryableAsync()
        {
            return await Task.FromResult(Mapper.Map<IQueryable<IVehicleMakeModel>>(repository.GetAllQueryableAsync()));
        }

    }
}