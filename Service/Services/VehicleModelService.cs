using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using Service.Interfaces;
using Service.Models.Entity;
using AutoMapper;
using Service.Interfaces.Services;
using Service.Interfaces.Models;

namespace Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private VehicleDBContext db = new VehicleDBContext();

        public IEnumerable<IVehicleMake> GetMakes()
        {
            IEnumerable<VehicleMakeEntity> makeItemsEntity = db.Makers.ToList().OrderByDescending(s => s.Name);
            IEnumerable<IVehicleMake> make = Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<IVehicleMake>>(makeItemsEntity);
            return make;
        }


        public StaticPagedList<IVehicleModel> GetVehicleDataPaged(ISystemDataModel systemDataModel)
        {
            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                systemDataModel.Page = 1;
            }
            else
            {
                systemDataModel.SearchValue = systemDataModel.CurrentFilter;
            }

            IQueryable<VehicleModelEntity> modelItems = from s in this.db.Models select s;

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

            IEnumerable<IVehicleModel> data = Mapper.Map<IEnumerable<VehicleModelEntity>, IEnumerable<IVehicleModel>>(modelItems);
            data = data.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            StaticPagedList<IVehicleModel> staticPagedList = new StaticPagedList<IVehicleModel>(data, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);

            return staticPagedList;
        }

        public void Create(IVehicleModel model)
        {
            VehicleModelEntity modelEntity = Mapper.Map<IVehicleModel, VehicleModelEntity>(model);
            this.db.Models.Add(modelEntity);
            this.Save();
        }

        public IVehicleModel Read(Guid? id)
        {
            return Mapper.Map<VehicleModelEntity, IVehicleModel>(db.Models.Find(id));
        }

        public void Update(IVehicleModel model)
        {
            VehicleModelEntity modelEntity = Mapper.Map<IVehicleModel, VehicleModelEntity>(model);
            this.db.Entry(modelEntity).State = EntityState.Modified;
            this.Save();
        }

        public void Delete(Guid? id)
        {
            this.db.Models.Remove(db.Models.Find(id));
            this.Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

    


        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

