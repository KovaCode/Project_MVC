using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model.Common;

namespace Repository.Commons
{
    public interface IRepository<T> where T : IEntity
    {

        IUnitOfWork UnitOfWork { get; }

        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> Table { get; }
    }

    //IUnitOfWork UnitOfWork { get; }

    //IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
    //IEnumerable<TEntity> Get<TEntity,TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex,  int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class;
    //IEnumerable<TEntity> Get<TEntity,TOrderBy>(Expression<Func<TEntity, bool>> criteria,Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class;
    //IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
    //TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
    //int Count<TEntity>() where TEntity : class;


    //void AddAsync<TEntity>(TEntity entity) where TEntity : class;
    //void Update<TEntity>(TEntity entity) where TEntity : class;
    //void Delete<TEntity>(TEntity entity) where TEntity : class;
    //void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
}
