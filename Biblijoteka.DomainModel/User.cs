using System.ComponentModel.DataAnnotations;

namespace Biblioteka.DomainModel
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        [UIHint("DropDownList")]
        [Required]
        public int LibraryID { get; set; }


        [UIHint("DropDownList")]
        public string Position { get; set; }

        public virtual Library Library { get; set; }
    }
}