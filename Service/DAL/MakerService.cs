using Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Service.Content.Filtering;

namespace Service.DAL
{
    public class MakerService : IVehicle<VehicleMake>
    {
        private VehicleDBContext db = new VehicleDBContext();

        public IEnumerable<VehicleMake> GetMakers(string sortOrder, string currentFilter, string value, int? page)
        {
            if (value != null)
            {
                page = 1;
            }
            else
            {
                value = currentFilter;
            }

            var makeItems = from s in db.Makers select s;

            if (!String.IsNullOrEmpty(value))
            {
                makeItems = makeItems.Where(s => s.Name.Contains(value) || s.Abrv.Contains(value));
            }

            switch (sortOrder)
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

        public IEnumerable<VehicleMake> GetMakers(Search search, Pagination pagination)
        {
            if (search != null)
            {
                pagination.Page = 1;
            }
            else
            {
                search.SearchValue = search.CurrentFilter;
            }

            var makeItems = from s in db.Makers select s;

            if (!String.IsNullOrEmpty(search.SearchValue))
            {
                makeItems = makeItems.Where(s => s.Name.Contains(search.SearchValue) || s.Abrv.Contains(search.SearchValue));
            }

            switch (search.SortOrder)
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

