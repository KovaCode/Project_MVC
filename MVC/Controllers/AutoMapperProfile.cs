using AutoMapper;
using Service.Models;
using MVC.Models;
using System.Collections.Generic;

namespace MVC.Models
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<List<VehicleMake>, IEnumerable<VehicleMakeView>>();
                cfg.CreateMap<List<VehicleModel>, IEnumerable<VehicleModelView>>();
                //cfg.CreateMap<VehicleMakeView, VehicleModelView>()
                //.ForMember(dest => dest.VehicleMakeName, opt => opt.MapFrom(src => src.Name))
                //.ForMember(dest =>dest.VehicleMakeId, opt => opt.MapFrom(src => src.Id));
            });

            IMapper mapper = config.CreateMapper();
        }
    }
}
