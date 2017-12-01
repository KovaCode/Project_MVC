using AutoMapper;
using Service.Models;
using MVC.Models;
using System.Collections.Generic;

namespace MVC.Models
{
    public class AutoMapperProfile : Profile
    {
        private IMapper mapper;
      
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
            mapper = config.CreateMapper();
        }
    }
}
