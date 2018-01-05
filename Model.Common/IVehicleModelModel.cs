using System;

namespace Model.Common
{
    public interface IVehicleModelModel : IVehicleBase
    {
        Guid VehicleMakeId { get; set; }
    }
}
