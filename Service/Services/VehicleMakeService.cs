using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using System.Collections;
using AutoMapper;
using Service.Services;
using Service.Models.Entity;

namespace Service.Servicess
{
    public class VehicleMakeService : IVehicle<IVehicleMake>
    {
        private VehicleDBContext db = new VehicleDBContext();

<<<<<<< HEAD
        public void Create(IVehicleMake make)
        {
            db.Makes.Add(make);
            Save();
        }

        public void Delete(Guid? id)
        {
            IVehicleMake make = db.Makes.Find(id);
            db.Makes.Remove(make);
            Save();
        }

        public IEnumerable<IVehicleMake> FindMake()
        {
            return db.Makes.ToList().OrderBy(s => s.Name);
        }

        public IEnumerable<IVehicleMake> GetVehicleData()
        {
            return db.Makes.ToList().OrderBy(s => s.Name);
        }

=======
        public IEnumerable<IVehicleMake> GetMakes()
        {
            IEnumerable<VehicleMakeEntity> makeItemsEntity = db.Makers.ToList().OrderBy(s => s.Name);
            IEnumerable<IVehicleMake> make = Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<IVehicleMake>>(makeItemsEntity);
            return make;
        }

        public IEnumerable<IVehicleMake> GetVehicleData()
        {
            IEnumerable<VehicleMakeEntity> makeItemsEntity = db.Makers.ToList().OrderBy(s => s.Name);
            IEnumerable<IVehicleMake> make = Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<IVehicleMake>>(makeItemsEntity);
            return make;
        }

>>>>>>> StaticPagging
        public IEnumerable<IVehicleMake> GetVehicleData(ISystemDataModel systemDataModel)
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
            IQueryable<VehicleMakeEntity> makeItems = from s in db.Makers select s;
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
            return makeItems.AsEnumerable<IVehicleMake>();
        }

        public IPagedList<IVehicleMake> GetVehicleDataPaged(ISystemDataModel systemDataModel)
        {
            IEnumerable<IVehicleMake> data = GetVehicleData(systemDataModel);
            return data.ToPagedList(systemDataModel.Page, systemDataModel.ResultsPerPage);
=======
            systemDataModel.TotalCount = makeItems.Count();

            IEnumerable<IVehicleMake> make = Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<IVehicleMake>>(makeItems);

            return make;
        }
        
        public StaticPagedList<IVehicleMake> GetVehicleDataPaged(ISystemDataModel systemDataModel) 
        {
            IEnumerable<IVehicleMake> data = GetVehicleData(systemDataModel);

            data = data.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);
     
            StaticPagedList<IVehicleMake> staticPagedList = new StaticPagedList<IVehicleMake>(data, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);

            return staticPagedList;
        }

        public void Create(IVehicleMake make)
        {
            VehicleMakeEntity makeEntity = Mapper.Map<IVehicleMake, VehicleMakeEntity>(make);
            db.Makers.Add(makeEntity);
            Save();
>>>>>>> StaticPagging
        }

        public IVehicleMake Read(Guid? id)
        {
<<<<<<< HEAD
            throw new NotImplementedException();
        }

        public void Update(IVehicleMake make)
        {
            db.Entry(make).State = EntityState.Modified;
=======
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
>>>>>>> StaticPagging
            Save();
        }


        public void Save()
        {
            db.SaveChanges();
        }







        //    public IEnumerable<VehicleMake> FindMake()
        //    {
        //        return db.Makes.ToList().OrderBy(s => s.Name);
        //    }

        //    public IEnumerable<IVehicleMake> GetVehicleData()
        //    {
        //        return db.Makes.ToList().OrderBy(s => s.Name);
        //    }

        //    public IEnumerable<IVehicleMake> GetVehicleData(ISystemDataModel systemDataModel)
        //    {
        //        if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
        //        {
        //            systemDataModel.Page = 1;
        //        }
        //        else
        //        {
        //            systemDataModel.SearchValue = systemDataModel.CurrentFilter;
        //        }

        //        var makeItems = from s in db.Makes select s;

        //        if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
        //        {
        //            makeItems = makeItems.Where(s => s.Name.Contains(systemDataModel.SearchValue));
        //        }

        //        if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
        //        {
        //            makeItems = makeItems.OrderBy(s => s.Name);
        //        }
        //        else
        //        {
        //            makeItems = makeItems.OrderByDescending(s => s.Name);
        //        }

        //        return makeItems.AsEnumerable<IVehicleMake>();
        //    }

        //    public IPagedList<IVehicleMake> GetVehicleDataPaged(ISystemDataModel systemDataModel) 
        //    {
        //        IEnumerable<IVehicleMake> data = GetVehicleData(systemDataModel);
        //        return data.ToPagedList(systemDataModel.Page, systemDataModel.ResultsPerPage);
        //    }

        //    public void Create(IVehicleMake make)
        //    {
        //        db.Makes.Add(make);
        //        Save();
        //    }

        //    public IVehicleMake Read(Guid? id)
        //    {
        //        return db.Makes.Find(id);
        //    }

        //    public void Update(IVehicleMake Makes)
        //    {
        //        db.Entry(Makes).State = EntityState.Modified;
        //        Save();
        //    }

        //    public void Delete(Guid? id)
        //    {
        //        VehicleMake make = db.Makes.Find(id);
        //        db.Makes.Remove(make);
        //        Save();

        //    }

        //    public void Save()
        //    {
        //        db.SaveChanges();
        //    }

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