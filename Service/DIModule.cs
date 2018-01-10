using DAL;
using Model;
using Model.Common;
using Repository.Commons.Models;
using Service.Common.Services;
using Service.Services;

namespace Service
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        #region Methods

        public override void Load()
        {
            //AutoMapper.Mapper.CreateMap<ProductEntity, Product>().ReverseMap();
            //AutoMapper.Mapper.CreateMap<ProductEntity, IProduct>().ReverseMap();
            //AutoMapper.Mapper.CreateMap<IProduct, Product>().ReverseMap();
            //AutoMapper.Mapper.CreateMap<CartEntity, Cart.Model.Cart>().ReverseMap();
            //AutoMapper.Mapper.CreateMap<CartEntity, ICart>().ReverseMap();
            //AutoMapper.Mapper.CreateMap<ICart, Cart.Model.Cart>().ReverseMap();


            //Bind<IVehicleDBContext>().To<VehicleDBContext>().InSingletonScope();
            Bind<IVehicleMakeService>().To<VehicleMakeService>();
            Bind<IVehicleModelService>().To<VehicleModelService>();
            //Bind<IVehicleMakeModel>().To<VehicleMakeModel>();
            //Bind<IVehicleModelModel>().To<VehicleModelModel>();
        }

        #endregion Methods
    }
}
