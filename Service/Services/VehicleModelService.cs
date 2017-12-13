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

<<<<<<< HEAD
  

        public void Create(IVehicleModel model)
        {
            db.Models.Add(model);
            Save();
        }

        public void Delete(Guid? id)
        {
            IVehicleModel model = db.Models.Find(id);
            db.Models.Remove(model);
            Save();
        }

 

        public IEnumerable<IVehicleMake> FindMake()
        {
            return db.Makes.ToList().OrderBy(s => s.Name);
        }

        public IEnumerable<IVehicleModel> GetVehicleData()
        {
            return db.Models.ToList().OrderBy(s => s.Name);
        }

=======
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

>>>>>>> StaticPagging
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

<<<<<<< HEAD
            var makeItems = from s in db.Makes select s;
=======
            IQueryable<VehicleModelEntity> modelItems = from s in this.db.Models select s;
>>>>>>> StaticPagging

            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                makeItems = makeItems.Where(s => s.Name.Contains(systemDataModel.SearchValue));
            }

            if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
            {
                makeItems = makeItems.OrderBy(s => s.Name);
            }
            else
            {
                makeItems = makeItems.OrderByDescending(s => s.Name);
            }

<<<<<<< HEAD
            return makeItems.AsEnumerable<IVehicleModel>();
        }

        public IPagedList<IVehicleModel> GetVehicleDataPaged(ISystemDataModel systemDataModel)
        {
            IEnumerable<IVehicleModel> data = GetVehicleData(systemDataModel);
            return data.ToPagedList(systemDataModel.Page, systemDataModel.ResultsPerPage);
        }

        public IVehicleModel Read(Guid? id)
        {
            return db.Models.Find(id);
        }


        public void Update(IVehicleModel model)
        {
            db.Entry(model).State = EntityState.Modified;
            Save();
        }

=======
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
>>>>>>> StaticPagging

        public void Save()
        {
            db.SaveChanges();
        }



        //public IEnumerable<VehicleMake> FindMake()
        //{
        //    return db.Makes.OrderBy(s => s.Name);
        //}

        //public IEnumerable<IVehicleModel> GetVehicleData()
        //{
        //    return this.Db.Models.ToList<IVehicleModel>();
        //}

        //public IEnumerable<IVehicleModel> GetVehicleData(ISystemDataModel systemDataModel)
        //{
        //    if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
        //    {
        //        systemDataModel.Page = 1;
        //    }
        //    else
        //    {
        //        systemDataModel.SearchValue = systemDataModel.CurrentFilter;
        //    }

        //    IEnumerable<IVehicleModel> modelItems = from s in this.Db.Models select s;

        //    if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
        //    {
        //        modelItems = modelItems.Where(s => s.Name.Contains(systemDataModel.SearchValue) || s.Abrv.Contains(systemDataModel.SearchValue) || s.Make.Name.Contains(systemDataModel.SearchValue));
        //    }


        //    if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
        //    {
        //        modelItems = modelItems.OrderBy(s => s.Name);
        //    }
        //    else
        //    {
        //        modelItems = modelItems.OrderByDescending(s => s.Name);
        //    }
        //    return modelItems;
        //}

        //public IPagedList<IVehicleModel> GetVehicleDataPaged(ISystemDataModel systemDataModel)
        //{
        //    IEnumerable<IVehicleModel> data = GetVehicleData(systemDataModel);
        //    return data.ToPagedList(systemDataModel.Page, systemDataModel.ResultsPerPage);
        //}

        //public void Create(IVehicleModel model)
        //{
        //    this.Db.Models.Add(model);
        //    this.Save();
        //}

        //public IVehicleModel Read(Guid? id)
        //{
        //    return this.Db.Models.Find(id);
        //}

        //public void Update(IVehicleModel model)
        //{
        //    this.Db.Entry(model).State = EntityState.Modified;
        //    this.Save();
        //}

        //public void Delete(Guid? id)
        //{
        //    IVehicleModel model = this.Read(id);
        //    this.Db.Models.Remove(model);
        //    this.Save();
        //}

        //public void Save()
        //{
        //    this.Db.SaveChanges();
        //}


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

