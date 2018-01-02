using MVC;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MVC.Controllers;
using Ninject;
using Ninject.Web.Common.WebHost;
using System.Reflection;
using Service.Servicess;
using Service.Common.Services;

namespace MVC_Project
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected new void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();
        }



        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            //kernel.Bind<IDbContext>().To<IocDbContext>().InRequestScope();
            //kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();

            kernel.Bind<IVehicleMakeService>().To<VehicleMakeService>();
            kernel.Bind<IVehicleModelService>().To<IVehicleModelService>();
            return kernel;
        }
    }


}
