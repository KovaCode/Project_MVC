using System.Collections.Generic;
using DAL;
using Model;
using Repository.Commons.Models;
using Repository.Patterns;
using Model.Common;
using Repository.Commons.Patterns;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Service.Common;
using DAL.Entity;
using PagedList;
using Common;

namespace Repository
{
    public class VehicleModelRepository :  IVehicleModelRepository
    {
        VehicleDBContext context;
        IGenericRepository<VehicleModelEntity> repository;
        IGenericRepository<VehicleMakeEntity> repositoryMake;

        public VehicleModelRepository(VehicleDBContext context)
        {
            this.context = context;
            repository = new GenericRepository<VehicleModelEntity>(context);
            repositoryMake = new GenericRepository<VehicleMakeEntity>(context);
        }

        public async Task<IEnumerable<IVehicleModelModel>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<IVehicleModelModel>>(await repository.GetAllAsync());
        }

        public async Task<IEnumerable<IVehicleModelModel>> GetAllAsync(ISystemDataModel systemDataModel)
        {
            IQueryable<VehicleModelEntity> makeItems = await repository.GetAllQueryableAsync();

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
            return await Task.FromResult(Mapper.Map<IEnumerable<IVehicleModelModel>>(makeItems.AsEnumerable()));
        }

        public async Task<IQueryable<IVehicleModelModel>> GetAllQueryableAsync()
        {
            return Mapper.Map<IQueryable<IVehicleModelModel>>(await repository.GetAllQueryableAsync());
        }

        public async Task<StaticPagedList<IVehicleModelModel>> GetAllPagedAsync(ISystemDataModel systemDataModel)
        {
            {
                if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
                {
                    systemDataModel.Page = 1;
                }
                else
                {
                    systemDataModel.SearchValue = systemDataModel.CurrentFilter;
                }

                IQueryable<VehicleModelEntity> items = await repository.GetAllQueryableAsync();

                if (!String.IsNullOrWhiteSpace(systemDataModel.SearchValue))
                {
                    items = items.Where(s => s.Name.Contains(systemDataModel.SearchValue, StringComparison.OrdinalIgnoreCase) || s.Make.Name.Contains(systemDataModel.SearchValue, StringComparison.OrdinalIgnoreCase));
                }

                if (!String.IsNullOrWhiteSpace(systemDataModel.SortOrder))
                {
                    items = items.OrderByDescending(s => s.Name);
                }
                else
                {
                    items = items.OrderBy(s => s.Name);
                }

                systemDataModel.TotalCount = items.Count();

                items = items.Skip((systemDataModel.Page - 1) * systemDataModel.ResultsPerPage).Take(systemDataModel.ResultsPerPage);

                StaticPagedList<VehicleModelEntity> list = new StaticPagedList<VehicleModelEntity>(items, systemDataModel.Page, systemDataModel.ResultsPerPage, systemDataModel.TotalCount);

                return Mapper.Map<StaticPagedList<IVehicleModelModel>>(list);
            }
        }

        public async Task<int> CreateAsync(IVehicleModelModel entity)
        {
            return await repository.CreateAsync(Mapper.Map<VehicleModelEntity>(entity));

        }

        public async Task<IVehicleModelModel> ReadAsync(Guid? id)
        {
            return Mapper.Map<IVehicleModelModel>(await repository.ReadAsync(id));
        }

        public async Task<int> UpdateAsync(IVehicleModelModel entity)
        {
            return await repository.UpdateAsync(Mapper.Map<VehicleModelEntity>(entity));
        }

        public async Task<int> DeleteAsync(Guid? id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<int> DeleteAsync(IVehicleModelModel entity)
        {
            return await repository.DeleteAsync(Mapper.Map<VehicleModelEntity>(entity));
        }

        public async Task<IEnumerable<IVehicleMakeModel>> GetAllMakesAsync()
        {
            return Mapper.Map<IEnumerable<IVehicleMakeModel>>(await repositoryMake.GetAllAsync());
        }

 
    }
}