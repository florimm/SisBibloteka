using System;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;

namespace Biblioteka.Services
{
    public class BookService : IDomainService<Book>
    {
        private readonly ICurrent current;

        public BookService(ICurrent _current)
        {
            current = _current;
        }

        public bool Save(Book currentObj, Book previousObj)
        {
            throw new NotImplementedException();
        }
    }

    public interface IDomainService<in T> where T : class 
    {
        bool Save(T currentObj, T previousObj);
    }
}
