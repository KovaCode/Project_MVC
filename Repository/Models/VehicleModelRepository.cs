using System.Collections.Generic;
using DAL;
using Model;
using Repository.Commons.Models;
using Repository.Patterns;
using Model.Common;
using Repository.Commons.Patterns;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Service.Common;

namespace Repository
{
    public class VehicleModelRepository :  IVehicleModelRepository
    {
        VehicleDBContext context;
        IGenericRepository<IVehicleModelModel> repository;

        public VehicleModelRepository(VehicleDBContext context)
        {
            this.context = context;
            repository = new GenericRepository<IVehicleModelModel>(context);
        }

        public async Task<int> CreateAsync(IVehicleModelModel entity)
        {
           return await repository.CreateAsync(entity);
        }

        public Task<int> UpdateAsync(IVehicleModelModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(Guid? id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<int> DeleteAsync(IVehicleModelModel entity)
        {
            return await repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<IVehicleModelModel>> GetAll()
        {
            return await repository.GetAllQueryableAsync();
        }

        public IEnumerable<IVehicleMakeModel> GetAllMakes()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<IVehicleModelModel>> GetAllQueryableAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IVehicleModelModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IVehicleModelModel> ReadAsync(Guid? id)
        {
            throw new NotImplementedException();
        }
    }
}