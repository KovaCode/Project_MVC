using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace MVC_Project.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            Mapper.Initialize(cfg =>
            cfg.CreateMap<Maker, Model>()
            .ForMember(dest => dest.MakeID, opt => opt.MapFrom(src => src.Id)));
                       
        }
    }
}
