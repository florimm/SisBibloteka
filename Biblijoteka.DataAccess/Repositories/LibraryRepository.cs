using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Repositories
{
    public interface ILibraryRepository : IRepository<Library>
    {
    }

    public class LibraryRepository : Repository<Library>, ILibraryRepository
    {
    }
}