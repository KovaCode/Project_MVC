using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;

namespace Service.DAL
{
    public class MakerService : IVehicle<VehicleMake>
    {
        private VehicleDBContext db = new VehicleDBContext();


        public IEnumerable<VehicleMake> FindMake()
        {
            return db.Makers.ToList().OrderBy(s => s.Name);
        }

        public IEnumerable<VehicleMake> GetVehicleData()
        {
            return db.Makers.ToList().OrderBy(s => s.Name);
        }

        public IEnumerable<VehicleMake> GetVehicleData(ISystemDataModel systemDataModel)
        {
            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                systemDataModel.Page = 1;
            }
            else
            {
                systemDataModel.SearchValue = systemDataModel.CurrentFilter;
            }

            IEnumerable<VehicleMake> makeItems = from s in db.Makers select s;

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

            return makeItems;
        }

        public IPagedList<VehicleMake> GetVehicleDataPaged(ISystemDataModel systemDataModel) 
        {
            IEnumerable<VehicleMake> data = GetVehicleData(systemDataModel);
            return data.ToPagedList(systemDataModel.Page, systemDataModel.ResultsPerPage);
        }

        public void Create(VehicleMake maker)
        {
            db.Makers.Add(maker);
            Save();
        }

        public VehicleMake Read(Guid? id)
        {
            return db.Makers.Find(id);
        }

        public void Update(VehicleMake maker)
        {
            db.Entry(maker).State = EntityState.Modified;
            Save();
        }

        public void Delete(Guid? id)
        {
            VehicleMake maker = db.Makers.Find(id);
            db.Makers.Remove(maker);
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

