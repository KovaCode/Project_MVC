using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Service.Services;
using Model.Common;
using Repository.Commons;

namespace Repository
{

    public class Repository2<T> : IRepository<T> where T : IEntity
    {
        public IEnumerable<T> Table => throw new NotImplementedException();

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
