using AutoMapper;
using MVC.Models;
using PagedList;
using System.Collections.Generic;
using Model.Common;
using DAL.Entity;
using System.Threading.Tasks;

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

                cfg.CreateMap<IVehicleMakeModel, VehicleMakeEntity>().ReverseMap();
                cfg.CreateMap<VehicleMakeEntity, VehicleMakeView>().ReverseMap();
                cfg.CreateMap<IVehicleMakeModel, VehicleMakeView>().ReverseMap();

                // MODEL - mappings /

                cfg.CreateMap<IVehicleModelModel, VehicleModelEntity>().ReverseMap();
                cfg.CreateMap<VehicleModelEntity, VehicleModelView>().ReverseMap();
                cfg.CreateMap<IVehicleModelModel, VehicleModelView>().ReverseMap();

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