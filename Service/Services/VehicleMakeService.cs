using Service.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using AutoMapper;
using Service.Common.Services;
using System.Threading.Tasks;
using Model.Common;
using Model;
using Repository.Commons.Models;

namespace Service.Services
{
    public class VehicleMakeService/*<TEntity>*/ : IVehicleMakeService //where TEntity : IVehicleModelModel
    {
        private readonly IVehicleMakeRepository repository;

        public VehicleMakeService(IVehicleMakeRepository repository)
        {
            this.repository = repository;           
        }

        public async Task<StaticPagedList<IVehicleMakeModel>> GetVehicleDataPagedAsync(ISystemDataModel systemDataModel)
        {
            StaticPagedList<IVehicleMakeModel> items = await repository.GetAllPagedAsync(systemDataModel);
            systemDataModel.TotalCount = items.Count();
            return items;
        }

        public async Task<int> CreateAsync(IVehicleMakeModel item)
        {
            return await repository.CreateAsync(item);
        }

        public async Task<IVehicleMakeModel> ReadAsync(Guid? id)
        {
            return await repository.ReadAsync(id);
        }

        public async Task<int> UpdateAsync(IVehicleMakeModel item)
        {
            return await repository.UpdateAsync(item);
        }

        public async Task<int> DeleteAsync(Guid? id)
        {
           return await repository.DeleteAsync(id);
        }

    }
}