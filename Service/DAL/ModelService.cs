using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using Service.Models;
using Service.DAL;

namespace Service.DAL
{
    public class ModelService : IVehicle<VehicleModel>
    {
        private VehicleDBContext db;

        public ModelService(VehicleDBContext context)
        {
            this.db = context;
        }

   

        public IQueryable<VehicleModel> GetModelsQueryable()
        {
            return from s in this.db.Models select s;
        }


        public VehicleMake GetMakerById(int? id)
        { 
            return this.db.Makers.Find(id);
        }

        public IEnumerable<VehicleMake> GetAllMakers()
        {
            return this.db.Makers.ToList<VehicleMake>();
        }


        public IEnumerable<VehicleModel> GetModels(string sortOrder, string currentFilter, string search, int? page)
        {
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            var modelItems = GetModelsQueryable();

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

            return modelItems;
        }

        public IEnumerable<VehicleModel> GetModels()
        {
            return this.db.Models.ToList<VehicleModel>();
        }

        public VehicleModel Read(int? id)
        {
            return this.db.Models.Find(id);
        }

        public void Update(VehicleModel model)
        {
            this.db.Entry(model).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public void Delete(int? id)
        {
            VehicleModel model = this.Read(id);
            this.db.Models.Remove(model);
        }


        public void Save()
        {
            this.db.SaveChanges();
        }

        public void Create(VehicleModel model)
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

