using Model.Common;
using System;

namespace Model
{
    public class VehicleModel : IVehicleModel
    {
        public Guid Id { get; set; }
        public Guid VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual VehicleMake Make { get; set; }
    }

}
