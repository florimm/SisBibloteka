using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;

namespace Biblioteka.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IEFContext DB { get; set; }
        public ILogger logger { get; set; }

        public string Errors { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return DB.DataSet<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> _expression)
        {
            return GetAll().Where(_expression);
        }

        public T GetByID(object id)
        {
            return DB.DataSet<T>().Find(id);
        }

        public virtual void Save(T _obj)
        {
            
        }

        public void Update(T _obj)
        {
            DB.Entry(_obj).State = EntityState.Modified;
        }

        public void Insert(T _obj)
        {
            DB.Entry(_obj).State = EntityState.Added;
        }

        public bool Commit()
        {
            try
            {
                bool saved = DB.SaveChanges() > 0;
                return saved;
            }
            catch (DbEntityValidationException e)
            {
                var sb = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    string errLine = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    sb.Append(errLine);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        string errSubLine = string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        sb.Append(errSubLine);
                    }
                }
                Errors = sb.ToString();
                return false;
            }
        }

        public void Delete(T _obj)
        {
            DB.Entry(_obj).State = EntityState.Deleted;
        }
    }
}