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

namespace Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        IGenericRepository<IVehicleMakeModel> repository;

        public VehicleMakeRepository(VehicleDBContext context)
        {
            repository = new GenericRepository<IVehicleMakeModel>(context);
        }

        public async Task CreateAsync(IVehicleMakeModel entity)
        {
           await repository.CreateAsync(entity);
        }

        public async Task DeleteAsync(Guid? id)
        {
            await repository.DeleteAsync(id);
        }

        public async Task DeleteAsync(IVehicleMakeModel entity)
        {
            await repository.DeleteAsync(entity);
        }

        public IEnumerable<IVehicleMakeModel> GetAll()
        {
            return repository.GetAll();
        }

        public IQueryable<IVehicleMakeModel> GetAllQueryable()
        {
            return repository.GetAllQueryable();
        }

        public Task<IVehicleMakeModel> GetById(Guid? id)
        {
          return repository.GetById(id);
        }

        public async Task UpdateAsync(IVehicleMakeModel entity)
        {
            await repository.UpdateAsync(entity);
        }


    }
}