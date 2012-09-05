// -----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="">
// </copyright>
// -----------------------------------------------------------------------

using System.Linq.Expressions;

namespace Biblioteka.DataAccess.Repositories
{
    using System;
    using System.Linq;

    public interface IRepository<T> where T : class
    {
        IEFContext DB { get; set; }
        string Errors { get; set; }
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T,bool>> _expression);
        T GetByID(object id);
        void Save(T _obj);
        void Update(T _obj);
        void Insert(T _obj);
        bool Commit();
        void Delete(T _obj);
    }
}
