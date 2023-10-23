using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resume.Models
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .HasKey(e => e.Id)
                .HasName("PK_Contact");

            builder.Property(e => e.Name)
                .IsRequired();
            builder.Property(e => e.TelephoneNumber)
                .IsRequired();
            builder.Property(e => e.Email)
                .IsRequired();

            Contact contact = new()
            {
                Id = 1,
                Name = "Васнецов Виктор Михайлович",
                TelephoneNumber = "+7 (495) 305-18-48",
                Email = "info@vasnetsov.com"
            };

            builder.HasData(new Contact[] { contact });

            builder.ToTable("Contacts");
        }
    }
}