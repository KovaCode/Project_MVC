using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVC_Project.DAL
{
    public class ModelService : IVehicle<Model>
    {
        private VehicleDBContext db;

        public ModelService(VehicleDBContext context)
        {
            db = context;
        }


        public IQueryable<Model> getModelsQueryable()
        {
            return from s in db.Models select s;
        }


        public IEnumerable<Model> getModels()
        {
            return db.Models.ToList<Model>();
        }

        public Model Read(int? id)
        {
            return db.Models.Find(id);
        }

        public void Update(Model model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? id)
        {
            Model model = this.Read(id);
            db.Models.Remove(model);
            this.Save();
        }


        public void Save()
        {
            db.SaveChanges();
        }

        public void Create(Model model)
        {
            db.Models.Add(model);
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

