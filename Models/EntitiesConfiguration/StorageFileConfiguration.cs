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
                    Name = $"work1",
                    Extension = "jpg"
                },
                new StorageFile
                {
                    Id = 2,
                    Name = $"work2",
                    Extension = "jpg"
                },
                new StorageFile
                {
                    Id = 3,
                    Name = $"work3",
                    Extension = "jpg"
                },
                new StorageFile
                {
                    Id = 4,
                    Name = $"work4",
                    Extension = "jpg"
                },
                new StorageFile
                {
                    Id = 5,
                    Name = $"work5",
                    Extension = "jpg"
                },
                new StorageFile
                {
                    Id = 6,
                    Name = $"work6",
                    Extension = "jpg"
                }
            };

            builder.HasData(files);

            builder.ToTable("StorageFiles");
        }
    }
}