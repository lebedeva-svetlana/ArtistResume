using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resume.Models
{
    public class StorageFileConfiguration : IEntityTypeConfiguration<StorageFile>
    {
        public void Configure(EntityTypeBuilder<StorageFile> builder)
        {
            builder
                .HasKey(e => e.Id)
                .HasName("PK_StorageFile");

            builder.Property(e => e.Name)
                .IsRequired();
            builder.Property(e => e.Extension)
                .IsRequired();

            List<StorageFile> files = new()
            {
                new StorageFile
                {
                    Id = 1,
                    Name = $"Алёнушка",
                    Extension = "jpg"
                },
                new StorageFile
                {
                    Id = 2,
                    Name = $"Витязь",
                    Extension = "jpg"
                },
                new StorageFile
                {
                    Id = 3,
                    Name = $"Иван-Царевич",
                    Extension = "jpg"
                },
                new StorageFile
                {
                    Id = 4,
                    Name = $"Ковёр-Самолёт",
                    Extension = "jpg"
                },
                new StorageFile
                {
                    Id = 5,
                    Name = $"Три царевны",
                    Extension = "jpg"
                },
                new StorageFile
                {
                    Id = 6,
                    Name = $"Богатыри",
                    Extension = "jpg"
                }
            };

            builder.HasData(files);

            builder.ToTable("StorageFiles");
        }
    }
}