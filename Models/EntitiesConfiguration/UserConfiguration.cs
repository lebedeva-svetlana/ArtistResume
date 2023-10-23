using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resume.Models
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(e => e.Id)
                .HasName("PK_User");

            builder.Property(e => e.Email)
                .IsRequired();
            builder.Property(e => e.Password)
                .IsRequired();

            User user = new()
            {
                Id = 1,
                Email = "test@mail.com",
                Password = "1234"
            };

            builder.HasData(new User[] { user });

            builder.ToTable("Users");
        }
    }
}