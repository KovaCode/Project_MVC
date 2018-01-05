using Model.Common;
using System;

namespace Model
{
    public class VehicleMakeModel : IVehicleMakeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }

}
