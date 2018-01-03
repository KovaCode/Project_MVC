using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Service.Services;

namespace Repository
{

    public class Repository<T> where T : BaseEntity
    {
        private UnitOfWork unitOfWork;
        private readonly VehicleDBContext context;
        private IDbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(VehicleDBContext context)
        {
            this.context = context;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public async Task InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                //this.Entities.Add(entity);
                //this.context.SaveChanges();
                await unitOfWork.AddAsync(entity);
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                //this.context.SaveChanges();
               await unitOfWork.UpdateAsync(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                //this.Entities.Remove(entity);
                //this.context.SaveChanges();
                await unitOfWork.DeleteAsync(entity);
                await unitOfWork.CommitAsync();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public virtual IEnumerable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<T>();
                }
                return entities;
            }
        }
    }

    //public class Repository<T> : IRepository<T> where T : BaseEntity
    //{
    //    private UnitOfWork unitOfWork;

    //    public Repository(IUnitOfWork unitOfWork)
    //    {
    //        unitOfWork = (UnitOfWork)unitOfWork;

    //    }

    //    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    //    public void AddAsync<TEntity>(TEntity entity) where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Count<TEntity>() where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<int> CreateAsynchAsync(T entity)
    //    {
    //        {
    //            try
    //            {
    //                if (entity == null)
    //                {
    //                    throw new ArgumentNullException("entity");
    //                }

    //                return await unitOfWork.AddAsync(entity);
    //            }
    //            catch (DbEntityValidationException dbEx)
    //            {
    //                var msg = string.Empty;

    //                foreach (var validationErrors in dbEx.EntityValidationErrors)
    //                {
    //                    foreach (var validationError in validationErrors.ValidationErrors)
    //                    {
    //                        msg += string.Format("Property: {0} Error: {1}",
    //                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
    //                    }
    //                }

    //                var fail = new Exception(msg, dbEx);
    //                throw fail;
    //            }
    //        }
    //    }

    //    public void Delete<TEntity>(TEntity entity) where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<int> DeleteAsynch(T entity)
    //    {
    //        {
    //            try
    //            {
    //                if (entity == null)
    //                {
    //                    throw new ArgumentNullException("entity");
    //                }
    //                await unitOfWork.DeleteAsync(entity);
    //                return await this.unitOfWork.CommitAsync();
    //            }
    //            catch (DbEntityValidationException dbEx)
    //            {
    //                var msg = string.Empty;

    //                foreach (var validationErrors in dbEx.EntityValidationErrors)
    //                {
    //                    foreach (var validationError in validationErrors.ValidationErrors)
    //                    {
    //                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
    //                        validationError.PropertyName, validationError.ErrorMessage);
    //                    }
    //                }
    //                var fail = new Exception(msg, dbEx);
    //                throw fail;
    //            }
    //        }
    //    }

    //    public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<TEntity> Get<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Update<TEntity>(TEntity entity) where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<int> UpdateAsynchAsync(T entity)
    //    {
    //        {
    //            try
    //            {
    //                if (entity == null)
    //                {
    //                    throw new ArgumentNullException("entity");
    //                }
    //                return await unitOfWork.UpdateAsync(entity);
    //            }
    //            catch (DbEntityValidationException dbEx)
    //            {
    //                var msg = string.Empty;

    //                foreach (var validationErrors in dbEx.EntityValidationErrors)
    //                {
    //                    foreach (var validationError in validationErrors.ValidationErrors)
    //                    {
    //                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
    //                        validationError.PropertyName, validationError.ErrorMessage);
    //                    }
    //                }
    //                var fail = new Exception(msg, dbEx);
    //                throw fail;
    //            }
    //        }
    //    }





}
