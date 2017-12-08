using AutoMapper;
using MVC;
using MVC.Models;
using Service.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PagedList;
using MVC.Controllers;

namespace MVC_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfiguration.Configure();

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<VehicleMake, VehicleMakeView>();
            //    cfg.CreateMap<VehicleMakeView, VehicleMake>();
            //    cfg.CreateMap<VehicleModel, VehicleModelView>();
            //    cfg.CreateMap<VehicleModelView, VehicleModel>();
            //    cfg.CreateMap(typeof(PagedList<VehicleMake>), typeof(PagedList<VehicleMakeView>)).ConvertUsing(typeof(PagedListConverter<VehicleMake, VehicleMakeView>));
            //    cfg.CreateMap(typeof(PagedList<VehicleModel>), typeof(PagedList<VehicleModelView>)).ConvertUsing(typeof(PagedListConverter<VehicleModel, VehicleModelView>));
            //}

            //);
        }
    }
}
