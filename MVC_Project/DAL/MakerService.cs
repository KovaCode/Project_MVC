using MVC_Project.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVC_Project.DAL
{
    public class MakerService : IVehicle<Maker>
    {

        private VehicleDBContext db;

        public MakerService(VehicleDBContext context)
        {
            db = context;
        }

        public IQueryable<Maker> getMakersQueryable()
        {
            return from s in db.Makers select s;
        }


        public IEnumerable<Maker> getMakers(string sortOrder, string currentFilter, string search, int? page)
        {
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
           
            var makeItems = getMakersQueryable();

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


        public IEnumerable<Maker> getMakers()
        {
            return db.Makers.ToList<Maker>();
        }

        public Maker Read(int? id)
        {
            return db.Makers.Find(id);
        }

        public void Update(Maker maker)
        {
            db.Entry(maker).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Maker maker = db.Makers.Find(id);
            db.Makers.Remove(maker);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Create(Maker maker)
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

