﻿using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;

namespace Repository.Commons.Patterns
{
    public interface IGenericRepository<T> where T : class
    {
        
        Task<IQueryable<T>> GetAllQueryableAsync();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(ISystemDataModel systemDataModel);
        Task<StaticPagedList<T>> GetAllPagedAsync(ISystemDataModel systemDataModel);
        Task<int> CreateAsync(T entity);
        Task<T> ReadAsync(Guid? id);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(Guid? id);
        Task<int> DeleteAsync(T entity);
    }
}
