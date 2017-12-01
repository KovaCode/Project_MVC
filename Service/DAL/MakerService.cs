using Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service.DAL
{
    public class MakerService : IVehicle<VehicleMake>
    {
        private VehicleDBContext db = new VehicleDBContext();

        public IEnumerable<VehicleMake> GetMakers(SystemDataModel systemDataModel)
        {
            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
                {
                systemDataModel.Page = 1;
            }
            else
            {
                systemDataModel.SearchValue = systemDataModel.CurrentFilter;
            }

            var makeItems = from s in db.Makers select s;

            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                makeItems = makeItems.Where(s => s.Name.Contains(systemDataModel.SearchValue) || s.Abrv.Contains(systemDataModel.SearchValue));
            }

            switch (systemDataModel.SortOrder)
            {
                case "name_desc":
                    makeItems = makeItems.OrderBy(s => s.Name);
                    break;
                case "name_asc":
                    makeItems = makeItems.OrderBy(s => s.Name);
                    break;

                case "Abrv":
                    makeItems = makeItems.OrderBy(s => s.Abrv);
                    break;
                default:
                    makeItems = makeItems.OrderBy(s => s.Name);
                    break;
            }
            return makeItems;
        }

        public IEnumerable<VehicleMake> GetMakers()
        {
            return db.Makers.ToList<VehicleMake>();
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

        public void Create(VehicleMake maker)
        {
            db.Makers.Add(maker);
            Save();
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

