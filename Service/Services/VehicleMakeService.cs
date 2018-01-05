using Service.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using AutoMapper;
using Service.Common.Services;
using System.Threading.Tasks;
using Repository.Commons;
using Model.Common;
using Model;
using Repository.Commons.Models;

namespace Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private IVehicleMakeRepository repository;

        public VehicleMakeService(IVehicleMakeRepository _repository)
        {
            this.repository = _repository;           
        }
        
        public IEnumerable<IVehicleMakeModel> GetMakes()
        {
            IEnumerable<IVehicleMakeModel> makeItemsEntity = repository.GetAll();
            IEnumerable<IVehicleMakeModel> make = Mapper.Map<IEnumerable<IVehicleMakeModel>, IEnumerable<IVehicleMakeModel>>(makeItemsEntity);
            return make;
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
            IQueryable<VehicleMakeModel> makeItems = repository.GetAllQueryable();

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

            IEnumerable<IVehicleMakeModel> data = Mapper.Map<IEnumerable<VehicleMakeModel>, IEnumerable<IVehicleMakeModel>>(makeItems);

            data = data.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            StaticPagedList<IVehicleMakeModel> staticPagedList = new StaticPagedList<IVehicleMakeModel>(data, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);

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
            VehicleMakeModel entity = await repository.GetByIdAsync(id);
            return Mapper.Map<VehicleMakeModel, IVehicleMakeModel>(entity);
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