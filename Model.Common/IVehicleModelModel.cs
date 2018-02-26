using System;

namespace Model.Common
{
    public interface IVehicleModelModel : IVehicleBase
    {
        Guid VehicleMakeId { get; set; }
        IVehicleMakeModel Make { get; set; }
    }
}