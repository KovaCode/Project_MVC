using PagedList;
using Service.Common.Models;
using System;
using System.Collections.Generic;

namespace Service.Common.Services
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
