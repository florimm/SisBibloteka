using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.DomainModel
{
    public class Borrower
    {
        public Borrower()
        {
            //this.Student = new Student();
        }
        public int ID { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public int BookCopyID { get; set; }
        [Required]
        public DateTime On { get; set; }

        public DateTime? PromiseReturn { get; set; }

        public DateTime? Return { get; set; }

        public int DaysLate
        {
            get
            {
                if (PromiseReturn.HasValue && Return.HasValue)
                    return (Return - PromiseReturn).Value.Days;
                return 0;
            }
        }

        [Required]
        public int Status { get; set; }

        public string Comment { get; set; }

        public virtual Student Student { get; set; }
        public virtual BookCopy BookCopy { get; set; }
    }
}