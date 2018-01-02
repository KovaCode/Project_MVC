using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Common;

namespace Repository.Commons
{
    public interface IRepository<T> where T : IEntity
    {
        T GetById(Guid id);

        //T Create(T entity);
        Task<int> CreateAsynchAsync(T entity);

        //T Read(Guid? id);
        Task<T> ReadAsynch(Guid? id);

        //void Update(T entity);
        Task<int> UpdateAsynch(T entity);

        //void Delete(T entity);
        Task<int> DeleteAsynch(T entity);

        //IEnumerable<T> GetTable { get; }
        Task<IEnumerable<T>> GetTableAsynch { get; }


    }
}
