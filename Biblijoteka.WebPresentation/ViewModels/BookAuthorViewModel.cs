using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biblioteka.DomainModel;

namespace Biblioteka.WebPresentation.ViewModels
{
    public class BookAuthorViewModel
    {
        public BookAuthorViewModel()
        {
        }
        public Book Book { get; set; }
        public Author Author { get; set; }
        public List<BookCopy> Libraries { get; set; }
    }
}