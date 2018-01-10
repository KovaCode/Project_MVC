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
        
        public IEnumerable<IVehicleMakeModel> GetMakes()
        {
            return  Mapper.Map<IEnumerable<IVehicleMakeModel>>(repository.GetAll());
        }

        public StaticPagedList<IVehicleMakeModel> GetVehicleDataPaged(ISystemDataModel systemDataModel)
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
            IQueryable<IVehicleMakeModel> makeItems = repository.GetAllQueryable();

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

            systemDataModel.TotalCount = makeItems.Count();

            //IEnumerable<IVehicleMakeModel> data = Mapper.Map<IEnumerable<IVehicleMakeModel>, IEnumerable<IVehicleMakeModel>>(makeItems);

            makeItems = makeItems.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            StaticPagedList<IVehicleMakeModel> staticPagedList = new StaticPagedList<IVehicleMakeModel>(makeItems, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);

            return staticPagedList;
        }

        public async Task CreateAsync(IVehicleMakeModel make)
        {
            VehicleMakeModel makeEntity = Mapper.Map<IVehicleMakeModel, VehicleMakeModel>(make);
            await repository.CreateAsync(makeEntity);
        }

        public async Task<IVehicleMakeModel> ReadAsync(Guid? id)
        {
            //return Mapper.Map<VehicleMakeEntity, IVehicleMake>(db.Makers.Find(id));
            IVehicleMakeModel entity = await repository.GetById(id);
            return entity; /*Mapper.Map<IVehicleMakeModel, IVehicleMakeModel>(entity)*/;
        }

        public async Task UpdateAsync(IVehicleMakeModel make)
        {
            VehicleMakeModel makeEntity = Mapper.Map<IVehicleMakeModel, VehicleMakeModel>(make);
            await repository.UpdateAsync(makeEntity);
        }

        public async Task DeleteAsync(Guid? id)
        {
            await repository.DeleteAsync(id);
        }
    }
}