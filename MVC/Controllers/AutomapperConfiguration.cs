using AutoMapper;
using MVC.Models;
using PagedList;
using Service.Models;
using Service.Models.Entity;
using Service.Interfaces;
using System.Collections.Generic;

namespace MVC.Controllers
{

        public class AutoMapperConfiguration
        {
            public static void Configure()
            {
                ConfigureItemMapping();
            }

            private static void ConfigureItemMapping()
            {
            Mapper.Initialize(cfg =>
            {
                // MAKE - mappings //               
                cfg.CreateMap<VehicleMake, IVehicleMake>();
                cfg.CreateMap<VehicleMakeEntity, IVehicleMake>();
                cfg.CreateMap<VehicleMakeView, IVehicleMake>();
                cfg.CreateMap<IVehicleMake, VehicleMakeView>();

                cfg.CreateMap<VehicleMakeEntity, VehicleMake>();
                cfg.CreateMap<VehicleMakeView, VehicleMake>();
                cfg.CreateMap<VehicleMake, VehicleMakeView>();

                // MODEL - mappings //
                cfg.CreateMap<VehicleModelEntity, VehicleModel>();
                cfg.CreateMap<IVehicleModel, VehicleModel>();
                cfg.CreateMap<VehicleModel, VehicleModelView>();
                cfg.CreateMap<VehicleModelView, VehicleModel>();


              
                cfg.CreateMap(typeof(StaticPagedList<IVehicleMake>), typeof(StaticPagedList<VehicleMakeView>)).ConvertUsing(typeof(PagedListConverter<IVehicleMake, VehicleMakeView>));

                cfg.CreateMap(typeof(StaticPagedList<VehicleMake>), typeof(StaticPagedList<VehicleMakeView>)).ConvertUsing(typeof(PagedListConverter<VehicleMake, VehicleMakeView>));
                cfg.CreateMap(typeof(StaticPagedList<VehicleModel>), typeof(StaticPagedList<VehicleModelView>)).ConvertUsing(typeof(PagedListConverter<VehicleModel, VehicleModelView>));
            }
            );
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
}