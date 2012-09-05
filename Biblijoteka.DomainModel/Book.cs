using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.DomainModel
{
    public class Book
    {

        [UIHint("HiddenInput")]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }
        
        public string ISBN10 { get; set; }

        public string ISBN13 { get; set; }

        [Required]
        public int Edition { get; set; }

        [UIHint("DateTime")]
        public DateTime? PublicationDate { get; set; }

        [UIHint("MultiLineText")]
        public string Description { get; set; }

        public string Publisher { get; set; }

        [Required]
        [UIHint("DropDownList"), Display(Name = "Author")]
        public int AuthorID { get; set; }

        public Author Author { get; set; }

        //public virtual ICollection<BookCopy> BooksCopies { get; set; }
    }
}
