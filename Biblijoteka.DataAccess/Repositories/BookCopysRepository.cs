using System.Data.Entity;
using System.Linq;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Repositories
{
    public interface IBookCopysRepository : IRepository<BookCopy>
    {

    }
    public class BookCopysRepository : Repository<BookCopy>, IBookCopysRepository
    {
        public override IQueryable<BookCopy> GetAll()
        {
            return DB.BooksCopys.Include(d => d.Library).Include(d=> d.Book);
        }
    }
}