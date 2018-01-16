using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using Service.Common;
using AutoMapper;
using Service.Common.Services;
using Model.Common;
using Model;
using System.Threading.Tasks;
using Repository.Commons.Models;

namespace Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        IVehicleModelRepository repository;
        
        public VehicleModelService(IVehicleModelRepository repository)
        {
                this.repository = repository;
        }

        public async Task<IEnumerable<IVehicleMakeModel>> GetAllMakeAsync()
        {
            return await repository.GetAllMakesAsync();
        }

        public async Task<StaticPagedList<IVehicleModelModel>> GetVehicleDataPagedAsync(ISystemDataModel systemDataModel)
        {
            StaticPagedList<IVehicleModelModel> items = await repository.GetAllPagedAsync(systemDataModel);
            systemDataModel.TotalCount = items.Count();
            return items;
        }

        public async Task<int> CreateAsync(IVehicleModelModel item)
        {
            return await repository.CreateAsync(item);
        }

        public async Task<IVehicleModelModel> ReadAsync(Guid? id)
        {
            return await repository.ReadAsync(id);
        }

        public async Task<int> UpdateAsync(IVehicleModelModel item)
        {
            return await repository.UpdateAsync(item);
        }

        public async Task<int> DeleteAsync(Guid? id)
        {
            return await repository.DeleteAsync(id);
        }


    }
}

