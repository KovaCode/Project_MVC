
using Service.Interfaces;
using System;
using System.Collections.Generic;
using PagedList;
using Service.Models;
using System.Linq.Expressions;

namespace Service.Interfaces
{
    public interface IVehicle<T> : IDisposable where T : class
    {
        void Create(T obj);
        T Read(Guid? id);
        void Update(T obj);
        void Delete(Guid? id);
        IEnumerable<IVehicleMake> FindMake();
        IEnumerable<T> GetVehicleData();
        IEnumerable<T> GetVehicleData(ISystemDataModel model);
        IPagedList<T> GetVehicleDataPaged(ISystemDataModel systemDataModel);
    }
}

