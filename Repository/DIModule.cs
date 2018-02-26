using DAL;
using Repository.Commons.Models;
using Repository.Commons.Patterns;
using Repository.Patterns;

namespace Repository
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        #region Methods

        public override void Load()
        {

            Bind<IVehicleDBContext>().To<VehicleDBContext>().InSingletonScope();            
            Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
        }
        #endregion Methods
    }
}