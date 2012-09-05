using System.Data.Entity.ModelConfiguration;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Mapps
{
    public class BorrowerMapper : EntityTypeConfiguration<Borrower>
    {
        public BorrowerMapper()
        {
            ToTable("Borrowers");
            HasKey(t => t.ID);

            Property(t => t.On).HasColumnName("On").IsRequired();
            Property(t => t.Return).HasColumnName("Return");
            Property(t => t.Status).HasColumnName("Status").IsRequired();
            Property(t => t.Comment).HasColumnName("Comment");
            Property(t => t.PromiseReturn).HasColumnName("PromiseReturn");

            HasRequired(c => c.BookCopy).WithMany().HasForeignKey(c => c.BookCopyID);
            HasRequired(c => c.Student).WithMany().HasForeignKey(c => c.StudentID);

        }
    }
}