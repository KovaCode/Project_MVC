using AutoMapper;
using MVC;
using MVC.Models;
using Service.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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
            //Mapper.Initialize(c => c.AddProfile<AutoMapperProfile>());

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VehicleMake, VehicleMakeView>();
                cfg.CreateMap<VehicleModel, VehicleModelView>();
                cfg.CreateMap<VehicleMakeView, VehicleModelView>()
                .ForMember(dest => dest.VehicleMakeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.VehicleMakeId, opt => opt.MapFrom(src => src.Id));
            }
            );
        }
    }
}
