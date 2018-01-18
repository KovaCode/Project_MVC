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
using PagedList;

namespace Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        IGenericRepository<VehicleMakeEntity> repository;

        public VehicleMakeRepository(VehicleDBContext context)
        {
            repository = new GenericRepository<VehicleMakeEntity>(context);
        }

        public async Task<IEnumerable<IVehicleMakeModel>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<IVehicleMakeModel>>(await repository.GetAllAsync());
        }
   
        public async Task<IEnumerable<IVehicleMakeModel>> GetAllAsync(ISystemDataModel systemDataModel)
        {
            return Mapper.Map<IEnumerable<IVehicleMakeModel>>(await repository.GetAllAsync(systemDataModel));    
        }

        public async Task<StaticPagedList<IVehicleMakeModel>> GetAllPagedAsync(ISystemDataModel systemDataModel)
        {
            return Mapper.Map<StaticPagedList<IVehicleMakeModel>>(await repository.GetAllPagedAsync(systemDataModel));
        }

        public async Task<IQueryable<IVehicleMakeModel>> GetAllQueryableAsync()
        {
            return Mapper.Map<IQueryable<IVehicleMakeModel>>(await repository.GetAllQueryableAsync());
        }

        public async Task<int> CreateAsync(IVehicleMakeModel entity)
        {
           return await repository.CreateAsync(Mapper.Map<VehicleMakeEntity>(entity));
            
        }

        public async Task<IVehicleMakeModel> ReadAsync(Guid? id)
        {
            return Mapper.Map<IVehicleMakeModel>(await repository.ReadAsync(id));
        }

        public async Task<int> UpdateAsync(IVehicleMakeModel entity)
        {
            ///*IVehicleMakeModel*/ item = await repository.ReadAsync(id);
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

   
    }
}