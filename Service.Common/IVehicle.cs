using System;
using System.Collections.Generic;
using PagedList;
using Service.Common.Models;

namespace Service.Common
{
    public interface IVehicle<T> : IDisposable where T : class
    {
        void Create(T obj);
        T Read(Guid? id);
        void Update(T obj);
        void Delete(Guid? id);       
        StaticPagedList<T> GetVehicleDataPaged(ISystemDataModel systemDataModel);
        IEnumerable<IVehicleMake> GetMakes();

    }
}

