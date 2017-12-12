using System.Data.Entity;
using Service.Interfaces;
using Service.Models;

namespace Service.Interfaces
{
    public interface IVehicleDBContext
    {
        IDbSet<IVehicleMake> Makes { get; }
        IDbSet<IVehicleModel> Models { get; }
    }
}