using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Repositories
{
    public interface IBorrowerRepository : IRepository<Borrower>
    {

    }

    public class BorrowerRepository : Repository<Borrower>, IBorrowerRepository
    {
    }

    public interface IStudentRepository : IRepository<Student>
    {
        
    }
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        
    }
}