using DAL;
using DAL.Entity;
using Model;
using Model.Common;
using Repository.Commons.Models;
using System.Collections.Generic;
using AutoMapper;
using PagedList;
using Repository.Commons.Patterns;
using Repository.Patterns;

namespace Repository
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        #region Methods

        public override void Load()
        {
            //Mapper.Initialize(cfg =>
            //{

            //    // MAKE - mappings //               
            //    cfg.CreateMap<IVehicleMakeModel, VehicleMakeModel>().ReverseMap();
            //    cfg.CreateMap<VehicleMakeModel, VehicleMakeEntity>().ReverseMap();
            //    cfg.CreateMap<VehicleMakeEntity, VehicleMakeModel>().ReverseMap();
            //    cfg.CreateMap<VehicleMakeEntity, IVehicleMakeModel>().ReverseMap();
            //    cfg.CreateMap<IVehicleMakeModel, VehicleMakeModel>().ReverseMap();

            //    // MODEL - mappings /
            //    cfg.CreateMap<VehicleModelEntity, VehicleModelModel>().ReverseMap();
            //    cfg.CreateMap<VehicleModelEntity, IVehicleModelModel>().ReverseMap();
            //    cfg.CreateMap<IVehicleModelModel, VehicleModelModel>().ReverseMap();

            //    cfg.CreateMap(typeof(StaticPagedList<IVehicleMakeModel>), typeof(StaticPagedList<VehicleMakeView>)).ConvertUsing(typeof(PagedListConverter<IVehicleMakeModel, VehicleMakeView>));
            //    cfg.CreateMap(typeof(StaticPagedList<IVehicleModelModel>), typeof(StaticPagedList<VehicleModelView>)).ConvertUsing(typeof(PagedListConverter<IVehicleModelModel, VehicleModelView>));

            //});


            Bind<IVehicleDBContext>().To<VehicleDBContext>().InSingletonScope();            
            Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
            

        }

        #endregion Methods

        //class PagedListConverter<TSource, TDestination> : ITypeConverter<StaticPagedList<TSource>, StaticPagedList<TDestination>> where TSource : class where TDestination : class
        //{
        //    public StaticPagedList<TDestination> Convert(StaticPagedList<TSource> source, StaticPagedList<TDestination> destination, ResolutionContext context)
        //    {
        //        var collection = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        //        return new StaticPagedList<TDestination>(collection, source.PageNumber, source.PageSize, source.TotalItemCount);
        //    }
        //}
    }
}