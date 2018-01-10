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

        public async Task CreateAsync(IVehicleModelModel entity)
        {
            await repository.CreateAsync(entity);
        }

        public async Task DeleteAsync(Guid? id)
        {
            await repository.DeleteAsync(id);
        }

        public async Task DeleteAsync(IVehicleModelModel entity)
        {
            await repository.DeleteAsync(entity);
        }

        public IEnumerable<IVehicleModelModel> GetAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<IVehicleMakeModel> GetAllMakes()
        {
            return Mapper.Map<IEnumerable<IVehicleMakeModel>>(context.Makers.AsEnumerable());
        }

        public IQueryable<IVehicleModelModel> GetAllQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<IVehicleModelModel> GetById(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IVehicleModelModel entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IVehicleMakeModel> IVehicleModelRepository.GetAllMakes()
        {
            throw new NotImplementedException();
        }
    }
}