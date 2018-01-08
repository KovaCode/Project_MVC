using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using Service.Common;
using AutoMapper;
using Service.Common.Services;
using DAL;
using DAL.Entity;
using Model.Common;
using Repository.Commons;
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


        public IEnumerable<Model.Common.IVehicleMakeModel> GetMakes()
        {
            IEnumerable<Model.VehicleMakeModel> makeItemsEntity = repository.GetAllMakes();
            IEnumerable<IVehicleMakeModel> make = Mapper.Map<IEnumerable<IVehicleMakeModel>, IEnumerable<IVehicleMakeModel>>(makeItemsEntity);
            return make;
        }


        public StaticPagedList<IVehicleModelModel> GetVehicleDataPaged(ISystemDataModel systemDataModel)
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

            IQueryable<VehicleModelModel> modelItems = repository.GetAllQueryable();

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

            IEnumerable<IVehicleModelModel> data = Mapper.Map<IEnumerable<VehicleModelModel>, IEnumerable<IVehicleModelModel>>(modelItems);
            data = data.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            StaticPagedList<IVehicleModelModel> staticPagedList = new StaticPagedList<IVehicleModelModel>(data, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);

            return staticPagedList;
        }

        public async Task CreateAsync(IVehicleModelModel model)
        {
            VehicleModelModel modelEntity = Mapper.Map<IVehicleModelModel, VehicleModelModel>(model);
            await repository.CreateAsync(modelEntity);
        }

        public async Task<IVehicleModelModel> ReadAsync(Guid? id)
        {
            //return Mapper.Map<VehicleModel, IVehicleModel>(db.Models.Find(id));

            VehicleModelModel vehicleModel = await repository.GetByIdAsync(id);
            return Mapper.Map<VehicleModelModel, IVehicleModelModel>(vehicleModel);

        }

        public async Task UpdateAsync(IVehicleModelModel model)
        {
            VehicleModelModel vehicleModel = Mapper.Map<IVehicleModelModel, VehicleModelModel>(model);

            await repository.UpdateAsync(vehicleModel);

        }

        public async Task DeleteAsync(Guid? id)
        {
            await repository.DeleteAsync(id);
        }

    }
}

