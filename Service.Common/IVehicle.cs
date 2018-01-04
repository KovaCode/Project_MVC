﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Common;
using PagedList;


namespace Service.Common
{
    public interface IVehicle<T> : IDisposable where T : class
    {
        Task CreateAsync(T obj);
        Task<T> ReadAsync(Guid? id);
        Task UpdateAsync(T obj);
        Task DeleteAsync(Guid? id);       
        StaticPagedList<T> GetVehicleDataPaged(ISystemDataModel systemDataModel);
        IEnumerable<IVehicleMake> GetMakes();

    }
}

