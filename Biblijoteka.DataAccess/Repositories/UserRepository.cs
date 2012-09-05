using System.Linq;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUserAndPassword(string userName, string password);
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public User GetByUserAndPassword(string userName, string password)
        {
            return GetAll(c => c.UserName == userName && c.Password == password).SingleOrDefault();
        }
    }

}