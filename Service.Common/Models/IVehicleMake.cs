using System;

namespace Service.Common.Models
{
    public interface IVehicleMake
    {
        Guid Id { get; set; }
        string Abrv { get; set; }       
        string Name { get; set; }
    }
}