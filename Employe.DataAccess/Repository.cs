using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Employee.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Employe.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext dbContext;
        private readonly DbSet<T> Dbset;

        public void Add(T entity)
        {
            Dbset.Add(entity);
        }
        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            Dbset = dbContext.Set<T>();
        }
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Any(predicate);
        }

        public void BulkInsert(List<T> entityList)
        {
            foreach (var t in entityList)
            {
                Dbset.Add(t);
            }
        }

        public int Count()
        {
            return Count(arg => true);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> iQueryable = Dbset
           .Where(predicate);
            return iQueryable.Count();
        }
        
        public void Delete(T entity, bool ForceDelete = false)
        {
            EntityEntry<T> dbEntityEntry = dbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                Dbset.Attach(entity);
                Dbset.Remove(entity);
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> iQueryable = Dbset
               .Where(predicate);
            return iQueryable.ToList().FirstOrDefault();
        }
        public T Get(int id)
        {            
            return Dbset.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> iQueryable = Dbset.Where(x => x != null);

            return iQueryable;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> iQueryable = Dbset
               .Where(predicate);
            return iQueryable;
        }

        public DbContext GetDbContext()
        {
            return dbContext;
        }


        public List<T> SendSql(string SqlQuery)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            Dbset.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
