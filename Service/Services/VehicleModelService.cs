﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using Service.Interfaces;
using Service.Models;
using Service.Models.Entity;
using AutoMapper;

namespace Service.Services
{
    public class VehicleModelService : IVehicle<IVehicleModel>
    {
        private VehicleDBContext db = new VehicleDBContext();

        public IEnumerable<IVehicleMake> GetMakes()
        {
            IEnumerable<VehicleMakeEntity> makeItemsEntity = db.Makers.ToList().OrderBy(s => s.Name);
            IEnumerable<IVehicleMake> make = Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<IVehicleMake>>(makeItemsEntity);
            return make;
        }

        public IEnumerable<IVehicleModel> GetVehicleData()
        {
            IEnumerable<VehicleModelEntity> modelItemsEntity = db.Models.ToList().OrderBy(s => s.Name);
            IEnumerable<IVehicleModel> model = Mapper.Map<IEnumerable<VehicleModelEntity>, IEnumerable<IVehicleModel>>(modelItemsEntity);
            return model;
        }

        public IEnumerable<IVehicleModel> GetVehicleData(ISystemDataModel systemDataModel)
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
                modelItems = modelItems.Where(s => s.Name.Contains(systemDataModel.SearchValue) || s.Abrv.Contains(systemDataModel.SearchValue) || s.Make.Name.Contains(systemDataModel.SearchValue));
            }

            if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
            {
                modelItems = modelItems.OrderBy(s => s.Name);
            }
            else
            {
                modelItems = modelItems.OrderByDescending(s => s.Name);
            }

            systemDataModel.TotalCount = modelItems.Count();

            IEnumerable<IVehicleModel> model = Mapper.Map<IEnumerable<VehicleModelEntity>, IEnumerable<IVehicleModel>>(modelItems);
            return model;
        }

        public StaticPagedList<IVehicleModel> GetVehicleDataPaged(ISystemDataModel systemDataModel)
        {
            IEnumerable<IVehicleModel> data = GetVehicleData(systemDataModel);

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
            this.db.SaveChanges();
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

