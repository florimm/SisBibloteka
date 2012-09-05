using System.Data.Entity.ModelConfiguration;
using Biblioteka.DomainModel;

namespace Biblioteka.DataAccess.Mapps
{
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            ToTable("Users");
            HasKey(t => t.ID);

            Property(t => t.UserName).HasColumnName("UserName")
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Password).HasColumnName("Password")
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Position).HasColumnName("Position")
                .IsRequired()
                .HasMaxLength(100);

            HasRequired(c => c.Library)
                .WithMany(c => c.Workers)
                .HasForeignKey(c => c.LibraryID);

        }
    }
}