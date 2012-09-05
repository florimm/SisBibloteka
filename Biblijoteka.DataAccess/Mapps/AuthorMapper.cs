using System.Data.Entity.ModelConfiguration;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Mapps
{
    public class AuthorMapper : EntityTypeConfiguration<Author>
    {
        public AuthorMapper()
        {
            ToTable("Auhtors");
            HasKey(t => t.ID);

            Property(t => t.Name).HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Surname).HasColumnName("Surname")
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Title).HasColumnName("Title")
                .HasMaxLength(20);
        }
    }
}