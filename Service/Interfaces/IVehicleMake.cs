using System;

namespace Service.Interfaces
{
    public interface IVehicleMake
    {
        Guid Id { get; set; }
        string Abrv { get; set; }       
        string Name { get; set; }
    }
}