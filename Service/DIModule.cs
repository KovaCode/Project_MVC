using Model;
using Model.Common;
using Service.Common.Services;
using Service.Services;


namespace Service
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        #region Methods

        public override void Load()
        {
            Bind<IVehicleMakeModel>().To<VehicleMakeModel>();
            Bind<IVehicleMakeService>().To<VehicleMakeService>();
            Bind<IVehicleModelService>().To<VehicleModelService>();
        }

        #endregion Methods
    }
}
