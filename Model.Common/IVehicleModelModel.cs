using System;
using System.Collections.Generic;
using Model;

namespace Model.Common
{
    public interface IVehicleModelModel : IVehicleBase
    {
        Guid VehicleMakeId { get; set; }
        IVehicleMakeModel Make { get; set; }
    }
}