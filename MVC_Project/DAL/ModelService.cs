using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;


namespace MVC_Project.DAL
{
    public class ModelService : IVehicle<Model>
    {
        private VehicleDBContext db;

        public ModelService(VehicleDBContext context)
        {
            this.db = context;
        }

   

        public IQueryable<Model> getModelsQueryable()
        {
            return from s in this.db.Models select s;
        }


        public Maker getMakerById(int? id)
        { 
            return this.db.Makers.Find(id);
        }

        public IEnumerable<Maker> getAllMakers()
        {
            return this.db.Makers.ToList<Maker>();
        }


        public IEnumerable<Model> getModels(string sortOrder, string currentFilter, string search, int? page)
        {
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            var modelItems = getModelsQueryable();

            if (!String.IsNullOrEmpty(search))
            {
                modelItems = modelItems.Where(s => s.Name.Contains(search) || s.Abrv.Contains(search));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    modelItems = modelItems.OrderBy(s => s.Name);
                    break;
                case "name_asc":
                    modelItems = modelItems.OrderBy(s => s.Name);
                    break;

                case "Abrv":
                    modelItems = modelItems.OrderBy(s => s.Abrv);
                    break;
                default:
                    modelItems = modelItems.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return modelItems.ToPagedList(pageNumber, pageSize);
        }

        public IEnumerable<Model> getModels()
        {
            return this.db.Models.ToList<Model>();
        }

        public Model Read(int? id)
        {
            return this.db.Models.Find(id);
        }

        public void Update(Model model)
        {
            this.db.Entry(model).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public void Delete(int? id)
        {
            Model model = this.Read(id);
            this.db.Models.Remove(model);
        }


        public void Save()
        {
            this.db.SaveChanges();
        }

        public void Create(Model model)
        {
            this.db.Models.Add(model);
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

