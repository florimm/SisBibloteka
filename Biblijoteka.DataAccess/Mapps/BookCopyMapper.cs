using System.Data.Entity.ModelConfiguration;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Mapps
{
    public class BookCopyMapper : EntityTypeConfiguration<BookCopy>
    {
        public BookCopyMapper()
        {
            ToTable("BooksCopys");
            HasKey(t => t.ID);

            Property(t => t.BookID).HasColumnName("BookID")
                .IsRequired();

            Property(t => t.LibraryID).HasColumnName("LibraryID")
                .IsRequired();

            Property(t => t.Copies).HasColumnName("Copies")
                .IsRequired();

            HasRequired(t => t.Book);
            HasRequired(t => t.Library);
        }
    }
}