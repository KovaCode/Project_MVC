using System;
using System.Collections.Generic;
using PagedList;
using Service.Interfaces.Models;

namespace Service.Interfaces.Services
{
    public interface IVehicleModelService : IVehicle<IVehicleModel>
    {
        new void Create(IVehicleModel obj);
        new IVehicleModel Read(Guid? id);
        new void Update(IVehicleModel obj);
        new void Delete(Guid? id);
        new StaticPagedList<IVehicleModel> GetVehicleDataPaged(ISystemDataModel systemDataModel);
        new IEnumerable<IVehicleMake> GetMakes();
    }
}
