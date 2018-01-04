using Service.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using AutoMapper;
using Service.Common.Services;
using Repository.Commons;
using System.Threading.Tasks;
using Model.Common;
using Model;

namespace Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        IMakeRepository repository;

        public VehicleMakeService(IMakeRepository _repository)
        {
            this.repository = _repository;           
        }
        
        public IEnumerable<IVehicleMake> GetMakes()
        {
            IEnumerable<VehicleMake> makeItemsEntity = repository.GetAll();
            IEnumerable<IVehicleMake> make = Mapper.Map<IEnumerable<VehicleMake>, IEnumerable<IVehicleMake>>(makeItemsEntity);
            return make;
        }

        public StaticPagedList<IVehicleMake> GetVehicleDataPaged(ISystemDataModel systemDataModel)
        {
            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                systemDataModel.Page = 1;
            }
            else
            {
                systemDataModel.SearchValue = systemDataModel.CurrentFilter;
            }

            //IQueryable<VehicleMakeEntity> makeItems = from s in db.Makers select s;
            IQueryable<VehicleMake> makeItems = repository.GetAllQueryable();

            if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
            {
                makeItems = makeItems.Where(s => s.Name.Contains(systemDataModel.SearchValue));
            }

            if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
            {
                makeItems = makeItems.OrderByDescending(s => s.Name);
            }
            else
            {
                makeItems = makeItems.OrderBy(s => s.Name);
            }

            systemDataModel.TotalCount = makeItems.Count();

            IEnumerable<IVehicleMake> data = Mapper.Map<IEnumerable<VehicleMake>, IEnumerable<IVehicleMake>>(makeItems);

            data = data.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

            StaticPagedList<IVehicleMake> staticPagedList = new StaticPagedList<IVehicleMake>(data, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);

            return staticPagedList;
        }

        public async Task CreateAsync(IVehicleMake make)
        {
            VehicleMake makeEntity = Mapper.Map<IVehicleMake, VehicleMake>(make);
            await repository.CreateAsync(makeEntity);

            //Save();
        }

        public async Task<IVehicleMake> ReadAsync(Guid? id)
        {
            //return Mapper.Map<VehicleMakeEntity, IVehicleMake>(db.Makers.Find(id));

            VehicleMake entity = await repository.GetByIdAsync(id);
            return Mapper.Map<VehicleMake, IVehicleMake>(entity);
        }

        public async Task UpdateAsync(IVehicleMake make)
        {
            VehicleMake makeEntity = Mapper.Map<IVehicleMake, VehicleMake>(make);
            //db.Entry(makeEntity).State = EntityState.Modified;

            await repository.UpdateAsync(makeEntity);

            //Save();
        }

        public async Task DeleteAsync(Guid? id)
        {
            //Make entity = repository.GetByIdAsync(id)
            await repository.DeleteAsync(id);
            //db.Makers.Remove(db.Makers.Find(id));
            //Save();

        }

        #region disposable

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}