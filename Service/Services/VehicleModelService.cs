using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using Service.Common;
using AutoMapper;
using Service.Common.Services;
using DAL;
using DAL.Entity;
using Model.Common;
using Repository.Commons;
using Model;
using System.Threading.Tasks;

namespace Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        IModelRepository repository;
        
        public VehicleModelService(IModelRepository repository)
        {
                this.repository = repository;
        }


        public IEnumerable<IVehicleMake> GetMakes()
        {
            IEnumerable<VehicleMake> makeItemsEntity = repository.GetAllMakes();
            IEnumerable<IVehicleMake> make = Mapper.Map<IEnumerable<VehicleMake>, IEnumerable<IVehicleMake>>(makeItemsEntity);
            return make;
        }


        public StaticPagedList<IVehicleModel> GetVehicleDataPaged(ISystemDataModel systemDataModel)
        {
            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                systemDataModel.Page = 1;
            }
            else
            {
                systemDataModel.SearchValue = systemDataModel.CurrentFilter;
            }

            //IQueryable<VehicleModelEntity> modelItems = from s in this.db.Models select s;

            IQueryable<VehicleModel> modelItems = repository.GetAllQueryable();

            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                modelItems = modelItems.Where(s => s.Name.Contains(systemDataModel.SearchValue) || s.Make.Name.Contains(systemDataModel.SearchValue));
            }

            if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
            {
                modelItems = modelItems.OrderByDescending(s => s.Name);
            }
            else
            {
                modelItems = modelItems.OrderBy(s => s.Name);
            }

            systemDataModel.TotalCount = modelItems.Count();

            IEnumerable<IVehicleModel> data = Mapper.Map<IEnumerable<VehicleModel>, IEnumerable<IVehicleModel>>(modelItems);
            data = data.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            StaticPagedList<IVehicleModel> staticPagedList = new StaticPagedList<IVehicleModel>(data, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);

            return staticPagedList;
        }

        public void CreateAsync(IVehicleModel model)
        {
            VehicleModel modelEntity = Mapper.Map<IVehicleModel, VehicleModel>(model);
            //this.db.Models.Add(modelEntity);
            repository.CreateAsync(modelEntity);

            //this.Save();
        }

        public async Task<IVehicleModel> ReadAsync(Guid? id)
        {
            //return Mapper.Map<VehicleModel, IVehicleModel>(db.Models.Find(id));

            VehicleModel vehicleModel = await repository.GetByIdAsync(id);
            return Mapper.Map<VehicleModel, IVehicleModel>(vehicleModel);

        }

        public async Task UpdateAsync(IVehicleModel model)
        {
            VehicleModel vehicleModel = Mapper.Map<IVehicleModel, VehicleModel>(model);

            await repository.UpdateAsync(vehicleModel);

            //this.db.Entry(modelEntity).State = EntityState.Modified;
            //this.Save();
        }

        public async Task DeleteAsync(Guid? id)
        {
            await repository.DeleteAsync(id);
            //this.db.Models.Remove(db.Models.Find(id));
            //this.Save();
        }

        //public void Save()
        //{
        //    db.SaveChanges();
        //}

    


        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        Task IVehicle<IVehicleModel>.CreateAsync(IVehicleModel obj)
        {
            throw new NotImplementedException();
        }

        Task<IVehicleModel> IVehicle<IVehicleModel>.ReadAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        Task IVehicle<IVehicleModel>.UpdateAsync(IVehicleModel obj)
        {
            throw new NotImplementedException();
        }

        Task IVehicle<IVehicleModel>.DeleteAsync(Guid? id)
        {
            throw new NotImplementedException();
        }
    }
}

