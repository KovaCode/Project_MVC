using System;

namespace Model.Common
{
    public interface IVehicleBase
    {
        Guid Id { get; set; }
        String Name { get; set; }
        String Abrv { get; set; }
    }
}
