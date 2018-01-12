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
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IVehicleMakeRepository repository;

        public VehicleMakeService(IVehicleMakeRepository repository)
        {
            this.repository = repository;           
        }
        
        public async Task<IEnumerable<IVehicleMakeModel>> GetMakesAsync()
        {
            return await Task.FromResult(Mapper.Map<IEnumerable<IVehicleMakeModel>>(repository.GetAllQueryableAsync()));
        }

        public async Task<StaticPagedList<IVehicleMakeModel>> GetVehicleDataPagedAsync(ISystemDataModel systemDataModel)
        {
            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                systemDataModel.Page = 1;
            }
            else
            {
                systemDataModel.SearchValue = systemDataModel.CurrentFilter;
            }

            //IQueryable<VehicleMakeEntity> makeItems = from s in db.Makers select s;
            IEnumerable<IVehicleMakeModel> makeItems = await repository.GetAllAsync(systemDataModel);

            //if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            //{
            //    makeItems = makeItems.Where(s => s.Name.Contains(systemDataModel.SearchValue));
            //}

            //if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
            //{
            //    makeItems = makeItems.OrderByDescending(s => s.Name);
            //}
            //else
            //{
            //    makeItems = makeItems.OrderBy(s => s.Name);
            //}

            systemDataModel.TotalCount = makeItems.Count();

            //IEnumerable<IVehicleMakeModel> data = Mapper.Map<IEnumerable<IVehicleMakeModel>, IEnumerable<IVehicleMakeModel>>(makeItems);

            makeItems = makeItems.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            StaticPagedList<IVehicleMakeModel> staticPagedList = new StaticPagedList<IVehicleMakeModel>(makeItems, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);

            return await Task.FromResult(staticPagedList);
        }

        public async Task<int> CreateAsync(IVehicleMakeModel make)
        {
            VehicleMakeModel makeEntity = Mapper.Map<IVehicleMakeModel, VehicleMakeModel>(make);
            return await repository.CreateAsync(makeEntity);
        }

        public async Task<IVehicleMakeModel> ReadAsync(Guid? id)
        {
            //return Mapper.Map<VehicleMakeEntity, IVehicleMake>(db.Makers.Find(id));
            IVehicleMakeModel entity = await repository.ReadAsync(id);
            return await Task.FromResult(entity); /*Mapper.Map<IVehicleMakeModel, IVehicleMakeModel>(entity)*/;
        }

        public async Task<int> UpdateAsync(IVehicleMakeModel make)
        {
            VehicleMakeModel makeEntity = Mapper.Map<IVehicleMakeModel, VehicleMakeModel>(make);
            return await repository.UpdateAsync(makeEntity);
        }

        public async Task<int> DeleteAsync(Guid? id)
        {
           return await repository.DeleteAsync(id);
        }
    }
}