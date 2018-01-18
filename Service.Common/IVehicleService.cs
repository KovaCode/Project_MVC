using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Common;
using PagedList;


namespace Service.Common
{
    public interface IVehicleService<T> where T : class
    {
        Task<int> CreateAsync(T obj);
        Task<T> ReadAsync(Guid? id);
        Task<int> UpdateAsync(T obj);
        Task<int> DeleteAsync(Guid? id);
        Task<IEnumerable<T>> GetVehicleDataAsync();
        Task<IEnumerable<T>> GetVehicleDataAsync(ISystemDataModel systemDataModel);
        Task<StaticPagedList<T>> GetVehicleDataPagedAsync(ISystemDataModel systemDataModel);
    }
}

