using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.DomainModel
{
    public class Library
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descriptions { get; set; }
        [Required]
        public string Location { get; set; }

        //public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<User> Workers { get; set; }
    }
}