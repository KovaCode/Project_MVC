using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using Service.Models;
using Service.DAL;

namespace Service.DAL
{
    public class ModelService : IVehicle<VechicleModel>
    {
        private VehicleDBContext db;

        public ModelService(VehicleDBContext context)
        {
            this.db = context;
        }

   

        public IQueryable<VechicleModel> getModelsQueryable()
        {
            return from s in this.db.Models select s;
        }


        public VechicleMake getMakerById(int? id)
        { 
            return this.db.Makers.Find(id);
        }

        public IEnumerable<VechicleMake> getAllMakers()
        {
            return this.db.Makers.ToList<VechicleMake>();
        }


        public IEnumerable<VechicleModel> getModels(string sortOrder, string currentFilter, string search, int? page)
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

        public IEnumerable<VechicleModel> getModels()
        {
            return this.db.Models.ToList<VechicleModel>();
        }

        public VechicleModel Read(int? id)
        {
            return this.db.Models.Find(id);
        }

        public void Update(VechicleModel model)
        {
            this.db.Entry(model).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public void Delete(int? id)
        {
            VechicleModel model = this.Read(id);
            this.db.Models.Remove(model);
        }


        public void Save()
        {
            this.db.SaveChanges();
        }

        public void Create(VechicleModel model)
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

