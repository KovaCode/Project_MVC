using System;
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
            IEnumerable<VehicleMake> make = Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<VehicleMake>>(makeItemsEntity);
            IEnumerable<IVehicleMake> iMake = Mapper.Map<IEnumerable<VehicleMake>, IEnumerable<IVehicleMake>>(make);

            return make;
        }



        public IEnumerable<IVehicleModel> GetVehicleData()
        {
            IEnumerable<VehicleModelEntity> modelItemsEntity = db.Models.ToList().OrderBy(s => s.Name);
            IEnumerable<VehicleModel> model = Mapper.Map<IEnumerable<VehicleModelEntity>, IEnumerable<VehicleModel>>(modelItemsEntity);
            IEnumerable<IVehicleModel> iModel = Mapper.Map<IEnumerable<VehicleModel>, IEnumerable<IVehicleModel>>(model);
            return iModel;
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

            IEnumerable<VehicleModelEntity> modelItems = from s in this.db.Models select s;

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
            IEnumerable<VehicleModel> model = Mapper.Map<IEnumerable<VehicleModelEntity>, IEnumerable<VehicleModel>>(modelItems);
            IEnumerable<IVehicleModel> iModel = Mapper.Map<IEnumerable<VehicleModel>, IEnumerable<IVehicleModel>>(model);

            return iModel;
        }

        public StaticPagedList<IVehicleModel> GetVehicleDataPaged(ISystemDataModel systemDataModel)
        {
            IEnumerable<IVehicleModel> data = GetVehicleData(systemDataModel);

            data = data.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            StaticPagedList<IVehicleModel> staticPagedList = new StaticPagedList<IVehicleModel>(data, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);
            //StaticPagedList<IVehicleModel> models = Mapper.Map<StaticPagedList<VehicleModelEntity>, StaticPagedList<IVehicleModel>>(staticPagedList);

            return staticPagedList;
            //return data.ToPagedList(systemDataModel.Page, systemDataModel.ResultsPerPage);
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
            this.db.Entry(model).State = EntityState.Modified;
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

