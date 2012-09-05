using System.Data.Entity;
using System.Linq;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
    }

    public class BookRepository : Repository<Book>, IBookRepository
    {
        public override IQueryable<Book> GetAll()
        {
            return DB.Books.Include(d=> d.Author);
        }

        public override void Save(Book b)
        {
            if(b.ID > 0)
            {
                Update(b);
            }
            else
            {
                Insert(b);
            }
        }
    }
}