﻿using DAL;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using MVC.App_Start;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Web;
using Repository;
using Service.Services;
using Service.Common.Services;

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
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }


        private static void RegisterServices(IKernel kernel)
        {
                kernel.Bind<VehicleDBContext>().ToSelf().InRequestScope();

                //kernel.Bind<Repository.Commons.IRepository>().To<EntityFrameworkRepository<VehicleDBContext>>().InRequestScope();
            

            kernel.Bind<IVehicleMakeService>().To<VehicleMakeService>();
            kernel.Bind<IVehicleModelService>().To<IVehicleModelService>();
            //kernel.Bind<IRepo>().ToMethod(ctx => new Repo("Ninject Rocks!"));
        }
    }
}