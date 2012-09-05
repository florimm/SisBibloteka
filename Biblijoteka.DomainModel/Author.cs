using System.ComponentModel.DataAnnotations;

namespace Biblioteka.DomainModel
{
    public class Author
    {
        public Author()
        {
            //Books = new Collection<Book>();
        }
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [UIHint("DropDownList")]
        public string Title { get; set; }

        //public virtual ICollection<Book> Books { get; set; }

        public string FullName { get { return Name + " " + Surname; }}
    }
}