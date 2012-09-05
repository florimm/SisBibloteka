using System.Data.Entity.ModelConfiguration;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Mapps
{
    public class StudentMapper : EntityTypeConfiguration<Student>
    {
        public StudentMapper()
        {
            ToTable("Students");
            HasKey(t => t.ID);

            Property(t => t.Name).HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Surname).HasColumnName("Surname")
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}