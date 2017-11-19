using Service.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service.DAL
{
    public class MakerService : IVehicle<VehicleMake>
    {

        private VehicleDBContext db;

        public MakerService(VehicleDBContext context)
        {
            db = context;
        }

        public IQueryable<VehicleMake> GetMakersQueryable()
        {
            return from s in db.Makers select s;
        }


        public IEnumerable<VehicleMake> GetMakers(string sortOrder, string currentFilter, string search, int? page)
        {
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
           
            var makeItems = GetMakersQueryable();

            if (!String.IsNullOrEmpty(search))
            {
                makeItems = makeItems.Where(s => s.Name.Contains(search) || s.Abrv.Contains(search));
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

            int pageSize = 10;
            int pageNumber = (page ?? 1);
                       
            return makeItems.ToPagedList(pageNumber, pageSize);
        }


        public IEnumerable<VehicleMake> GetMakers()
        {
            return db.Makers.ToList<VehicleMake>();
        }

        public VehicleMake Read(int? id)
        {
            return db.Makers.Find(id);
        }

        public void Update(VehicleMake maker)
        {
            db.Entry(maker).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            VehicleMake maker = db.Makers.Find(id);
            db.Makers.Remove(maker);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Create(VehicleMake maker)
        {
            db.Makers.Add(maker);
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

