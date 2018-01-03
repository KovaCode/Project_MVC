using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model.Common;

namespace Repository.Commons
{
    public interface IRepositoryMake<T> where T : IMake
    {
        IEnumerable<T> GetAll<IMake>();
  
        IEnumerable<T> Get<IMake, TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);

        IEnumerable<T> Get<MakeT, TOrderBy>(Expression<Func<T, bool>> criteria, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize,
            SortOrder sortOrder = SortOrder.Ascending);

        IEnumerable<T> Find<Make>(Expression<Func<T, bool>> criteria);

        T FindOne<TMake>(Expression<Func<T, bool>> criteria);

        int Count<TMake>();

        void AddAsync<Make>(T entity);

        void Update<Make>(T entity);

        void Delete<Make>(T entity);

        void Delete<Make>(Expression<Func<T, bool>> criteria);
    }
}
