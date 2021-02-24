using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Employee.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Employe.DataAccess
{
    public interface IRepository<T> where T : class
    {   /// <summary>
    /// Tüm veriyi Getir
    /// </summary>
        IQueryable<T> GetAll();
       
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        T Get(Expression<Func<T, bool>> predicate);
        T Get(int id);
        List<T> SendSql(string SqlQuery);



        void Add(T entity);

        void BulkInsert(List<T> entityList);
        void Update(T entity);
        void Delete(T entity, bool ForceDelete = false );

        bool Any(Expression<Func<T, bool>> expression);

        DbContext GetDbContext();     


    }
}
