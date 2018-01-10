using DAL;
using Repository.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
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


            Bind<IVehicleDBContext>().To<VehicleDBContext>().InSingletonScope();
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
        }

        #endregion Methods
    }
}