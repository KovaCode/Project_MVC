using Service.Models;
using System;

namespace Service.Interfaces
{
    public interface IVehicleModel
    {
        string Abrv { get; set; }
        Guid Id { get; set; }
        VehicleMake Make { get; set; }
        string Name { get; set; }
        Guid VehicleMakeId { get; set; }
    }
}