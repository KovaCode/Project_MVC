
using System;

namespace Service.DAL
{
    public interface IVehicle<T>
    {
        void Create(T obj);
        T Read(Guid? id);
        void Update(T obj);
        void Delete(Guid? id);
     
    }
}

