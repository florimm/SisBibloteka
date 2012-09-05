// -----------------------------------------------------------------------
// <copyright file="BookMapper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Mapps
{
    public class BookMapper : EntityTypeConfiguration<Book>
    {
        public BookMapper()
        {
            ToTable("Books");
            HasKey(t => t.ID);

            Property(t => t.Title).HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(200);

            Property(t => t.ISBN10).HasColumnName("ISBN10")
                .HasMaxLength(10);

            Property(t => t.ISBN13).HasColumnName("ISBN13")
                .HasMaxLength(14);

            Property(t => t.Edition).HasColumnName("Edition")
                .IsRequired();

            Property(t => t.PublicationDate).HasColumnName("PublicationDate")
                .IsOptional();

            Property(t => t.Description).HasColumnName("Description")
                .HasMaxLength(250);

            Property(t => t.Publisher).HasColumnName("Publisher")
                .HasMaxLength(250);

            Property(t => t.AuthorID).HasColumnName("AuthorID").IsRequired();

            HasRequired(t => t.Author)
                .WithMany()
                .HasForeignKey(d => d.AuthorID);

        }

    }
}
