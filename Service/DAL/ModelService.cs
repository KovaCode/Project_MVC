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
        private VehicleDBContext db = new VehicleDBContext();

        public VehicleMake GetMakerById(Guid? id)
        { 
            return this.db.Makers.Find(id);
        }

   

        public IEnumerable<VehicleMake> GetAllMakers()
        {
            return this.db.Makers.ToList<VehicleMake>().OrderBy(s=>s.Name);
        }

        public IEnumerable<VehicleModel> GetModels(SystemDataModel systemDataModel)
        {
            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                systemDataModel.Page = 1;
            }
            else
            {
                systemDataModel.SearchValue = systemDataModel.CurrentFilter;
            }

            var modelItems = from s in this.db.Models select s;

            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                modelItems = modelItems.Where(s => s.Name.Contains(systemDataModel.SearchValue) || s.Abrv.Contains(systemDataModel.SearchValue) || s.Make.Name.Contains(systemDataModel.SearchValue) );
            }


            if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
            {
                modelItems = modelItems.OrderBy(s => s.Name);
            }
            else
            {
                modelItems = modelItems.OrderByDescending(s => s.Name);
            }
            return modelItems;
        }

        public IEnumerable<VehicleModel> GetModels()
        {
            return this.db.Models.ToList<VehicleModel>();
        }

        public VehicleModel Read(Guid? id)
        {
            return this.db.Models.Find(id);
        }

        public void Update(VehicleModel model)
        {
            this.db.Entry(model).State = EntityState.Modified;
            this.Save();
        }

        public void Delete(Guid? id)
        {
            VehicleModel model = this.Read(id);
            this.db.Models.Remove(model);
            this.Save();
        }

        public void Save()
        {
            this.db.SaveChanges();
        }

        public void Create(VehicleModel model)
        {
            this.db.Models.Add(model);
            this.Save();
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

