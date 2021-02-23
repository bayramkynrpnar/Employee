using System;
using System.Collections.Generic;
using System.Text;

namespace Employe.DataAccess
{
   public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}
