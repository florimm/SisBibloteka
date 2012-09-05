using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using Biblioteka.DataAccess;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.Presentation.ViewModel;
using NSpec;

namespace Biblioteka.Tests
{
    public class main_view_model : nspec
    {
        private MainViewModel mainViewModel;
        void when_creating_new_instance()
        {
            //before = () => mainViewModel = new MainViewModel();
            //it["will not be null"] = () => mainViewModel.should_not_be(null);
            //it["default will start in debug mode"] = () => mainViewModel.IsInDesignMode.should_be(false);
        }
    }

    public class DummyContext : IEFContext
    {
        public IDbSet<Book> Books { get; set; }
        public IDbSet<Author> Authors { get; set; }
        public IDbSet<BookCopy> BooksCopys { get; set; }

        public IDbSet<Borrower> Borrowers { get; set; }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public IDbSet<T> DataSet<T>() where T : class
        {
            throw new NotImplementedException();
        }


        public IDbSet<User> Users
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<Student> Students { get; set; }

        IDbSet<T> IEFContext.DataSet<T>()
        {
            throw new NotImplementedException();
        }
    }
}
