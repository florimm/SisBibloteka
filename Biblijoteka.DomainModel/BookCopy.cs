using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.DomainModel
{
    public class BookCopy
    {
        public int ID { get; set; }

        [Required]
        [UIHint("DropDownList")]
        public int BookID { get; set; }

        [Required]
        [UIHint("DropDownList")]
        public int LibraryID { get; set; }

        [Required]
        public int Copies { get; set; }

        public Book Book { get; set; }

        public Library Library { get; set; }


    }
}