
using System;
using Repository.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Repository
{

    //public class Repository<T> : IRepository<T> where T : BaseEntity
    //{
    //    private UnitOfWork unitOfWork;
    //    //private IDbSet<T> entities;


    //    public Repository(IUnitOfWork unitOfWork)
    //    {
    //        unitOfWork = (UnitOfWork)unitOfWork;
    //    }

    //    public Task<IEnumerable<T>> GetTableAsynch()
    //    {
            
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

    //    public Task<int> UpdateAsynch(T entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    T GetById(Guid id)
    //        {
    //            throw new NotImplementedException();
    //        }

    //    T IRepository<T>.GetById(Guid id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    Task<T> ReadAsynch(Guid? id)
    //        {
    //            throw new NotImplementedException();
    //        }

    //    Task<T> IRepository<T>.ReadAsynch(Guid? id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    async Task<int> UpdateAsynchAsync(T entity)
    //        {
    //            {
    //                try
    //                {
    //                    if (entity == null)
    //                    {
    //                        throw new ArgumentNullException("entity");
    //                    }
    //                    return await unitOfWork.UpdateAsync(entity);
    //                }
    //                catch (DbEntityValidationException dbEx)
    //                {
    //                    var msg = string.Empty;
    //                    foreach (var validationErrors in dbEx.EntityValidationErrors)
    //                    {
    //                        foreach (var validationError in validationErrors.ValidationErrors)
    //                        {
    //                            msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
    //                            validationError.PropertyName, validationError.ErrorMessage);
    //                        }
    //                    }
    //                    var fail = new Exception(msg, dbEx);
    //                    throw fail;
    //                }
    //            }
    //        }


        //}
    }


