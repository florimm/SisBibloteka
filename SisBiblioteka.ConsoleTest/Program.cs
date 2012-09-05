using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Biblioteka.DataAccess;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using StructureMap;

namespace SisBiblioteka.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectFactory.Initialize(x=>
                {
                    x.For<IEFContext>().HybridHttpOrThreadLocalScoped().Use<EFContext>().Ctor<string>("connectionString").Is("Name=con"); ;
                    x.For<IUserRepository>().Use<UserRepository>();
                    x.For<IBookRepository>().Use<BookRepository>();
                    x.For<IAuthorRepository>().Use<AuthorRepository>();
                });
            //Database.SetInitializer(new DropCreateDatabaseAlways<EFContext>());
            //var db = ObjectFactory.GetInstance<IEFContext>();
            var db = new EFContext(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            var a = db.Borrowers.ToList();
            foreach (var author in a)
            {
                Console.WriteLine(author.Comment);
            }
        }
    }
}
