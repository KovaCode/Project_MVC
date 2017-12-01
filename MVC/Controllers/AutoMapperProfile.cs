using AutoMapper;
using Service.Models;
using MVC.Models;
using System.Collections.Generic;

namespace MVC.Models
{
    public class AutoMapperProfile : Profile
    {
        public static IMapper _mapper;
        

        public AutoMapperProfile()
        {
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<VehicleMake, VehicleMakeView>();
                x.CreateMap<VehicleModel, VehicleModelView>();
                x.CreateMap<VehicleMakeView, VehicleModelView>()               
                .ForMember(dest => dest.VehicleMakeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.VehicleMakeId, opt => opt.MapFrom(src => src.Id));


            });
            _mapper = config.CreateMapper();

        }

    }
}

//var config = new MapperConfiguration(cfg =>
//{
//    cfg.CreateMap<VehicleMake, VehicleMakeView>()
//    .ForMember(m => m.Id, map => map.MapFrom(vm => vm.Id))
//    .ForMember(m => m.Name, map => map.MapFrom(vm => vm.Name))
//    .ForMember(m => m.Abrv, map => map.MapFrom(vm => vm.Abrv));

//    cfg.CreateMap<VehicleModel, VehicleModelView>()
//    .ForMember(m => m.Id, map => map.MapFrom(vm => vm.Id))
//    .ForMember(m => m.Name, map => map.MapFrom(vm => vm.Name))
//    .ForMember(m => m.Abrv, map => map.MapFrom(vm => vm.Abrv));

//    cfg.CreateMissingTypeMaps = true;

//    cfg.CreateMap<VehicleModelView, VehicleMake>();

//});

////  config.AssertConfigurationIsValid();

//var mapper = config.CreateMapper();
