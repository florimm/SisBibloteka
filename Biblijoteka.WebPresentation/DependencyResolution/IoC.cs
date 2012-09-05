using Biblioteka.DataAccess;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using StructureMap;
namespace Biblioteka.WebPresentation {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                            x.For<IEFContext>().HttpContextScoped().Use<EFContext>().Ctor<string>("connectionString").Is("Name=con");
                            x.FillAllPropertiesOfType<IEFContext>();
                            x.For<IUserRepository>().Use<UserRepository>();
                            x.For<IBookRepository>().Use<BookRepository>();
                            x.For<IAuthorRepository>().Use<AuthorRepository>();
                            x.For<ILibraryRepository>().Use<LibraryRepository>();
                            x.For<IBookCopysRepository>().Use<BookCopysRepository>();
                            x.For<IBorrowerRepository>().Use<BorrowerRepository>();
                            x.For<IStudentRepository>().Use<StudentRepository>();
                        });
            return ObjectFactory.Container;
        }
    }
}