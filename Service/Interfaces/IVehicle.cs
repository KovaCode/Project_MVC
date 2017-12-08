
using Service.Interfaces;
using System;
using System.Collections.Generic;
using PagedList;
using Service.Models;

namespace Service.Interfaces
{
    public interface IVehicle<T>
    {
        void Create(T obj);
        T Read(Guid? id);
        void Update(T obj);
        void Delete(Guid? id);
        IEnumerable<VehicleMake> FindMake();
        IEnumerable<T> GetVehicleData();
        IEnumerable<T> GetVehicleData(ISystemDataModel model);
        IPagedList<T> GetVehicleDataPaged(ISystemDataModel systemDataModel);
    }
}

