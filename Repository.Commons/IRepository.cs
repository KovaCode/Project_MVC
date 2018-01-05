﻿using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Commons
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAllQueryable();
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(Guid? id);

        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid? id);
        Task DeleteAsync(T entity);

        
    }
}
