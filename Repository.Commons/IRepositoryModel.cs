using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model.Common;

namespace Repository.Commons
{
    public interface IRepositoryModel<Model> :IRepository<IModel>
    {
        IUnitOfWork UnitOfWork { get; }

        IEnumerable<T> GetAll<T>();
  
        IEnumerable<T> Get<T, TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);

        IEnumerable<T> Get<T, TOrderBy>(Expression<Func<T, bool>> criteria, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize,
            SortOrder sortOrder = SortOrder.Ascending);

        IEnumerable<T> Find<T>(Expression<Func<T, bool>> criteria);

        T FindOne<T>(Expression<Func<T, bool>> criteria);

        int Count<T>();

        void AddAsync<T>(T entity);

        void Update<T>(T entity);

        void Delete<T>(T entity);

        void Delete<T>(Expression<Func<T, bool>> criteria);
    }
}
