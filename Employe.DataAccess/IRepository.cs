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
       /// <summary>
       /// Veriyi where metodu ile getir
       /// </summary>
       /// <returns></returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Countunu bulur
        /// </summary>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// Verilen sorguya göre count bulur
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// istenilen veriyi getirir
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// İdsi alınan veriyi getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        T Get(int id);
        /// <summary>
        /// Sql sorgusu için
        /// </summary>
        /// <param name="SqlQuery"></param>
        /// <returns></returns>
        List<T> SendSql(string SqlQuery);


        /// <summary>
        /// ekleme yapmak için
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// Entity eklemek içim
        /// </summary>
        /// <param name="entityList"></param>
        void BulkInsert(List<T> entityList);
        /// <summary>
        /// Update işlemi
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// Delete İşlemi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ForceDelete"></param>
        void Delete(T entity, bool ForceDelete = false );

        /// <summary>
        /// Aynısı var mı kontrolü
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> expression);
        /// <summary>
        /// DbContexti verir
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();     


    }
}
