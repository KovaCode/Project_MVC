using System;

namespace Service.Interfaces
{
    public interface IVehicleMake
    {
        string Abrv { get; set; }
        Guid Id { get; set; }
        string Name { get; set; }
    }
}