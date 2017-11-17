﻿using AutoMapper;
using Service.Models;
using MVC.Models;

namespace MVC.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            Mapper.Initialize(cfg =>
            cfg.CreateMap<VechicleMake, VehicleMakeView>());

            //Mapper.Map.CreateMap<ModelView, Model>();
        }
    }
}
