using System;

namespace Service.Interfaces.Models
{
    public interface IVehicleModel
    {
        string Abrv { get; set; }
        Guid Id { get; set; }
        IVehicleMake Make { get; set; }
        string Name { get; set; }
        Guid VehicleMakeId { get; set; }
    }
}