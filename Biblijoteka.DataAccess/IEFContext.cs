using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess
{
    public interface IEFContext
    {
        IDbSet<Book> Books { get; set; }
        IDbSet<Author> Authors { get; set; }
        IDbSet<BookCopy> BooksCopys { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<Student> Students { get; set; }
        IDbSet<Borrower> Borrowers { get; set; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);

        IDbSet<T> DataSet<T>() where T : class;
    }
}