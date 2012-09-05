using System.Data.Entity.ModelConfiguration;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Mapps
{
    public class LibraryMapper : EntityTypeConfiguration<Library>
    {
        public LibraryMapper()
        {
            ToTable("Libraries");
            HasKey(t => t.ID);

            Property(t => t.Name).HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Descriptions).HasColumnName("Descriptions")
                .IsRequired();

            Property(t => t.Location).HasColumnName("Location");

        }
    }
}