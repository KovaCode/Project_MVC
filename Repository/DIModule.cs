using DAL;
using DAL.Entity;
using Model;
using Model.Common;
using Repository.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PagedList;

namespace Repository
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        #region Methods

        public override void Load()
        {
            Mapper.Initialize(cfg =>
            {

                // MAKE - mappings //               
                cfg.CreateMap<IVehicleMakeModel, VehicleMakeModel>().ReverseMap();
                cfg.CreateMap<VehicleMakeModel, VehicleMakeEntity>().ReverseMap();
                cfg.CreateMap<VehicleMakeEntity, VehicleMakeModel>().ReverseMap();
                cfg.CreateMap<VehicleMakeEntity, IVehicleMakeModel>().ReverseMap();
                cfg.CreateMap<IVehicleMakeModel, VehicleMakeModel>().ReverseMap();

                // MODEL - mappings /
                cfg.CreateMap<VehicleModelEntity, VehicleModelModel>().ReverseMap();
                cfg.CreateMap<VehicleModelEntity, IVehicleModelModel>().ReverseMap();
                cfg.CreateMap<IVehicleModelModel, VehicleModelModel>().ReverseMap();
            });


            Bind<IVehicleDBContext>().To<VehicleDBContext>().InSingletonScope();
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
            Bind<IVehicleMakeModel>().To<VehicleMakeModel>();

        }

        #endregion Methods
    }



    class PagedListConverter<TSource, TDestination> : ITypeConverter<StaticPagedList<TSource>, StaticPagedList<TDestination>> where TSource : class where TDestination : class
    {
        public StaticPagedList<TDestination> Convert(StaticPagedList<TSource> source, StaticPagedList<TDestination> destination, ResolutionContext context)
        {
            var collection = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
            return new StaticPagedList<TDestination>(collection, source.PageNumber, source.PageSize, source.TotalItemCount);
        }
    }



}