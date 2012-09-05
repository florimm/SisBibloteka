using System.Data.Entity;
using Biblioteka.DataAccess.Mapps;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess
{
    public class EFContext : DbContext, IEFContext
    {
        static EFContext()
		{
            Database.SetInitializer<EFContext>(null);
		}

        public EFContext()
        {
            Configuration.AutoDetectChangesEnabled = false;
        }

        public EFContext(string connectionString)
            : base(connectionString)
		{
		    Configuration.AutoDetectChangesEnabled = false;
		}


        public IDbSet<Book> Books { get; set; }
        public IDbSet<Author> Authors { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Student> Students { get; set; }
        public IDbSet<Borrower> Borrowers { get; set; }
        public IDbSet<Library> Libraries { get; set; }
        public IDbSet<BookCopy> BooksCopys { get; set; }

        public IDbSet<T> DataSet<T>() where T : class
        {
            return Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMapper());
            modelBuilder.Configurations.Add(new AuthorMapper());
            modelBuilder.Configurations.Add(new StudentMapper());
            modelBuilder.Configurations.Add(new UserMapper());
            modelBuilder.Configurations.Add(new BookCopyMapper());
            modelBuilder.Configurations.Add(new LibraryMapper());
            modelBuilder.Configurations.Add(new BorrowerMapper());
        }
    }
}
