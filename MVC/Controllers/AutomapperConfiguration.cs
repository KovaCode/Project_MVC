using AutoMapper;
using MVC.Models;
using PagedList;
using Service.Models;
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
                cfg.CreateMap<VehicleMake, VehicleMakeView>();
                cfg.CreateMap<VehicleMakeView, VehicleMake>();
                cfg.CreateMap<VehicleModel, VehicleModelView>();
                cfg.CreateMap<VehicleModelView, VehicleModel>();
                cfg.CreateMap(typeof(IPagedList<VehicleMake>), typeof(IPagedList<VehicleMakeView>)).ConvertUsing(typeof(PagedListConverter<VehicleMake, VehicleMakeView>));
                cfg.CreateMap(typeof(IPagedList<VehicleModel>), typeof(IPagedList<VehicleModelView>)).ConvertUsing(typeof(PagedListConverter<VehicleModel, VehicleModelView>));
            }
            );
        }


        class PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>> where TSource : class where TDestination : class
        {
            public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
            {
                var collection = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
                return new PagedList<TDestination>(collection, source.PageNumber, source.PageSize);
            }
        }


    }
}