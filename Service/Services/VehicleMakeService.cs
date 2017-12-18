using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using AutoMapper;
using Service.Services;
using Service.Models.Entity;
using Service.Interfaces.Services;
using Service.Interfaces.Models;

namespace Service.Servicess
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private VehicleDBContext db = new VehicleDBContext();

        public IEnumerable<IVehicleMake> GetMakes()
        {
            IEnumerable<VehicleMakeEntity> makeItemsEntity = db.Makers.ToList().OrderBy(s => s.Name);
            IEnumerable<IVehicleMake> make = Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<IVehicleMake>>(makeItemsEntity);
            return make;
        }

        public StaticPagedList<IVehicleMake> GetVehicleDataPaged(ISystemDataModel systemDataModel)
        {
            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                systemDataModel.Page = 1;
            }
            else
            {
                systemDataModel.SearchValue = systemDataModel.CurrentFilter;
            }

            IQueryable<VehicleMakeEntity> makeItems = from s in db.Makers select s;

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

            IEnumerable<IVehicleMake> data = Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<IVehicleMake>>(makeItems);

            data = data.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            StaticPagedList<IVehicleMake> staticPagedList = new StaticPagedList<IVehicleMake>(data, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);

            return staticPagedList;
        }

        public void Create(IVehicleMake make)
        {
            VehicleMakeEntity makeEntity = Mapper.Map<IVehicleMake, VehicleMakeEntity>(make);
            db.Makers.Add(makeEntity);
            Save();
        }

        public IVehicleMake Read(Guid? id)
        {
            return Mapper.Map<VehicleMakeEntity, IVehicleMake>(db.Makers.Find(id));
        }

        public void Update(IVehicleMake make)
        {
            VehicleMakeEntity makeEntity = Mapper.Map<IVehicleMake, VehicleMakeEntity>(make);
            db.Entry(makeEntity).State = EntityState.Modified;
            Save();
        }

        public void Delete(Guid? id)
        {
            db.Makers.Remove(db.Makers.Find(id));
            Save();

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