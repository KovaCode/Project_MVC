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

        public IEnumerable<IVehicleMakeModel> GetMakes()
        {
            IEnumerable<IVehicleMakeModel> makeItemsEntity = repository.GetAllMakes();
            IEnumerable<IVehicleMakeModel> make = Mapper.Map<IEnumerable<IVehicleMakeModel>, IEnumerable<IVehicleMakeModel>>(makeItemsEntity);
            return make;
        }

        public async Task<StaticPagedList<IVehicleModelModel>> GetVehicleDataPagedAsync(ISystemDataModel systemDataModel)
        {
            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                systemDataModel.Page = 1;
            }
            else
            {
                systemDataModel.SearchValue = systemDataModel.CurrentFilter;
            }

            //IQueryable<VehicleModelEntity> modelItems = from s in this.db.Models select s;

            IQueryable<IVehicleModelModel> modelItems = await repository.GetAllQueryableAsync();

            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                modelItems = modelItems.Where(s => s.Name.Contains(systemDataModel.SearchValue) || s.Make.Name.Contains(systemDataModel.SearchValue));
            }

            if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
            {
                modelItems = modelItems.OrderByDescending(s => s.Name);
            }
            else
            {
                modelItems = modelItems.OrderBy(s => s.Name);
            }

            systemDataModel.TotalCount = modelItems.Count();

            //IEnumerable<IVehicleModelModel> data = Mapper.Map<IEnumerable<VehicleModelModel>, IEnumerable<IVehicleModelModel>>(modelItems);
            modelItems = modelItems.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            return await Task.FromResult(new StaticPagedList<IVehicleModelModel>(modelItems, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount));
        }

        public async Task<int> CreateAsync(IVehicleModelModel model)
        {
            VehicleModelModel modelEntity = Mapper.Map<IVehicleModelModel, VehicleModelModel>(model);
            return await repository.CreateAsync(modelEntity);
        }

        public async Task<IVehicleModelModel> ReadAsync(Guid? id)
        {
            //return Mapper.Map<VehicleModel, IVehicleModel>(db.Models.Find(id));

            //VehicleModelModel vehicleModel = await repository.GetById(id);
            //return Mapper.Map<VehicleModelModel, IVehicleModelModel>(vehicleModel);

            return await repository.ReadAsync(id);

        }

        public async Task<int> UpdateAsync(IVehicleModelModel model)
        {
            VehicleModelModel vehicleModel = Mapper.Map<IVehicleModelModel, VehicleModelModel>(model);

            return await repository.UpdateAsync(vehicleModel);

        }

        public async Task<int> DeleteAsync(Guid? id)
        {
            return await repository.DeleteAsync(id);
        }

        public Task<IEnumerable<IVehicleMakeModel>> GetMakesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

