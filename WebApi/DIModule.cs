using DAL;
using Repository;
using Repository.Commons.Models;
using Repository.Commons.Patterns;
using Repository.Patterns;
using Service.Common.Services;
using Service.Services;

namespace WebApi
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            //Bind<IVehicleDBContext>().To<VehicleDBContext>().InSingletonScope();
            //Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            //Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            //Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
            Bind<IVehicleMakeService>().To<VehicleMakeService>();
            Bind<IVehicleModelService>().To<VehicleModelService>();

        }
    }
}