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
                cfg.CreateMap<VehicleMakeEntity, IVehicleMake>().ReverseMap();
                cfg.CreateMap<VehicleMakeView, IVehicleMake>().ReverseMap();

                // MODEL - mappings /
                cfg.CreateMap<VehicleModelEntity, IVehicleModel>().ReverseMap();
                cfg.CreateMap<VehicleModelView, IVehicleModel>().ReverseMap();
                
       
                cfg.CreateMap(typeof(StaticPagedList<IVehicleMake>), typeof(StaticPagedList<VehicleMakeView>)).ConvertUsing(typeof(PagedListConverter<IVehicleMake, VehicleMakeView>));
                cfg.CreateMap(typeof(StaticPagedList<IVehicleModel>), typeof(StaticPagedList<VehicleModelView>)).ConvertUsing(typeof(PagedListConverter<IVehicleModel, VehicleModelView>));
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