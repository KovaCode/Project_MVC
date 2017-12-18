using PagedList;
using Service.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Service.Interfaces.Services
{
    public interface IVehicleMakeService : IVehicle<IVehicleMake>
    {
        new void Create(IVehicleMake obj);
        new IVehicleMake Read(Guid? id);
        new void Update(IVehicleMake obj);
        new void Delete(Guid? id);
        new StaticPagedList<IVehicleMake> GetVehicleDataPaged(ISystemDataModel systemDataModel);
        new IEnumerable<IVehicleMake> GetMakes();
    }
}
