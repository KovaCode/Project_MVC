using DAL;
using Model;
using Repository.Commons;
using Service.Services;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Transactions;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected VehicleDBContext DbContext { get; private set; }

        public UnitOfWork(VehicleDBContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException("DbContext");
        }

        public virtual Task<int> AddAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    DbContext.Set<T>().Add(entity);
                }
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual Task<int> UpdateAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    DbContext.Set<T>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual Task<int> DeleteAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    DbContext.Set<T>().Attach(entity);
                    DbContext.Set<T>().Remove(entity);
                }
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual Task<int> DeleteAsync<T>(Guid id) where T : class
        {
            var entity = DbContext.Set<T>().Find(id);
            if (entity == null)
            {
                return Task.FromResult(1);
            }
            return DeleteAsync<T>(entity);
        }

        public async Task<int> CommitAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await DbContext.SaveChangesAsync();
                scope.Complete();
            }
            return result;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
