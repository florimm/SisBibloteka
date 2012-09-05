using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
    }

    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public override void Save(Author _obj)
        {
            if (_obj.ID > 0)
                Update(_obj);
            else
            {
                Insert(_obj);
            }
        }
    }
}