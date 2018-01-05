using AutoMapper;
using MVC.Models;
using PagedList;
using System.Collections.Generic;
using DAL.Entity;
using Model.Common;

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
                cfg.CreateMap<VehicleMakeEntity, IVehicleMakeModel>().ReverseMap();
                cfg.CreateMap<VehicleMakeView, IVehicleMakeModel>().ReverseMap();

                // MODEL - mappings /
                cfg.CreateMap<VehicleModelEntity, IVehicleModelModel>().ReverseMap();
                cfg.CreateMap<VehicleModelView, IVehicleModelModel>().ReverseMap();
                
       
                cfg.CreateMap(typeof(StaticPagedList<IVehicleMakeModel>), typeof(StaticPagedList<VehicleMakeView>)).ConvertUsing(typeof(PagedListConverter<IVehicleMakeModel, VehicleMakeView>));
                cfg.CreateMap(typeof(StaticPagedList<IVehicleModelModel>), typeof(StaticPagedList<VehicleModelView>)).ConvertUsing(typeof(PagedListConverter<IVehicleModelModel, VehicleModelView>));
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