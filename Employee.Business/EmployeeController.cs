using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Employe.DataAccess;
using Employee.Data.Models;

namespace Employee.Business
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly EmployeeDbContext DbContext;
        public Repository(EmployeeDbContext employeeDbContext)
        {
            DbContext = employeeDbContext;
        }
        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate);
        }

        public TEntity Get(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().RemoveRange(entities);
        }
        public class EFUnitOfWork : IUnitOfWork
        {
            private bool disposed = false;
            private readonly EmployeeDbContext dbContext;
            public EFUnitOfWork(EmployeeDbContext _dbContext)
            {
                if (_dbContext == null)
                    throw new ArgumentNullException($"{nameof(_dbContext)}" );
                dbContext = _dbContext;
            }
            
            


           
            public IRepository<TEntity1> GetRepository<TEntity1>() where TEntity1 : class
            {
                return new Repository<TEntity1>(dbContext);
            }

            public int SaveChanges()
            {
                try
                {
                    return dbContext.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {

                    throw;
                }
                catch(CommitFailedException ex)
                {
                    return dbContext.SaveChanges();
                }
                catch (EntityException ex)
                {
                    return dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            public virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        dbContext.Dispose();
                    }
                }
                this.disposed = true;
            }
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

        }
    }
}
