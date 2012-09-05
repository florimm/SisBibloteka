using System.ComponentModel.DataAnnotations;

namespace Biblioteka.DomainModel
{
    public class Student
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        public string FullName
        {
            get { return Name + " " + Surname; }
        }
    }
}