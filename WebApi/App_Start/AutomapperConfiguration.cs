using AutoMapper;
using System.Collections.Generic;
using Model.Common;
using DAL.Entity;
using WebApi.Models;
using Model;
using PagedList;

namespace WebApi.App_Start
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
                cfg.CreateMap<IVehicleMakeModel, VehicleMakeEntity>().ReverseMap();
                cfg.CreateMap<IVehicleModelModel, VehicleModelEntity>().ReverseMap();

                cfg.CreateMap<IVehicleMakeModel, MakeRestModel>().ReverseMap();
                cfg.CreateMap<IVehicleModelModel, ModelRestModel>().ReverseMap();

                cfg.CreateMap<VehicleMakeModel, MakeRestModel>().ReverseMap();
                cfg.CreateMap<VehicleModelModel, ModelRestModel>().ReverseMap();


                cfg.CreateMap(typeof(StaticPagedList<VehicleMakeEntity>), typeof(StaticPagedList<IVehicleMakeModel>)).ConvertUsing(typeof(PagedListConverter<VehicleMakeEntity, IVehicleMakeModel>));
                cfg.CreateMap(typeof(StaticPagedList<VehicleModelEntity>), typeof(StaticPagedList<IVehicleModelModel>)).ConvertUsing(typeof(PagedListConverter<VehicleModelEntity, IVehicleModelModel>));

                cfg.CreateMap(typeof(StaticPagedList<IVehicleMakeModel>), typeof(StaticPagedList<MakeRestModel>)).ConvertUsing(typeof(PagedListConverter<IVehicleMakeModel, MakeRestModel>));
                cfg.CreateMap(typeof(StaticPagedList<IVehicleModelModel>), typeof(StaticPagedList<ModelRestModel>)).ConvertUsing(typeof(PagedListConverter<IVehicleModelModel, ModelRestModel>));

                cfg.CreateMap(typeof(StaticPagedList<VehicleMakeModel>), typeof(StaticPagedList<MakeRestModel>)).ConvertUsing(typeof(PagedListConverter<VehicleMakeModel, MakeRestModel>));
                cfg.CreateMap(typeof(StaticPagedList<VehicleModelModel>), typeof(StaticPagedList<ModelRestModel>)).ConvertUsing(typeof(PagedListConverter<VehicleModelModel, ModelRestModel>));

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