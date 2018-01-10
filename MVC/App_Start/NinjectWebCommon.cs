using DAL;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using MVC.App_Start;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Web;
using Ninject.Extensions.Interception.Infrastructure.Language;
using System.Linq;
using Service;
using Service.Common.Services;
using Ninject.Web.WebApi;
using System.Web.Http;

    

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace MVC.App_Start
{

     public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var settings = new NinjectSettings();
            settings.LoadExtensions = true;
            settings.ExtensionSearchPatterns = settings.ExtensionSearchPatterns.Union(new string[] { "Service*.dll", "Repository*.dll" }).ToArray();
            var kernel = new StandardKernel(settings);
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                // Install Ninject-based IDependencyResolver into the Web API configuration to set Web API Resolver
                //GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind<Cart.Common.ICartInterceptor>().To<GreedyInterceptor>();
            //Note: This isn't a good way to obtain the Cart Service implementation type. This is Ninject limitation.
            //var makeService = kernel.Get<IVehicleMakeService>();
            //kernel.Rebind<IMakeService>().To(makeService.GetType()).Intercept().With<GreedyInterceptor>();
        }

    }
}