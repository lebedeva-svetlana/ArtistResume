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

            builder.Property(e => e.Path)
                .IsRequired();

            List<StorageFile> files = new()
            {
                new StorageFile
                {
                    Id = 1,
                    Path = $"work1.jpg"
                },
                new StorageFile
                {
                    Id = 2,
                    Path = $"work2.jpg"
                },
                new StorageFile
                {
                    Id = 3,
                    Path = $"work3.jpg"
                },
                new StorageFile
                {
                    Id = 4,
                    Path = $"work4.jpg"
                },
                new StorageFile
                {
                    Id = 5,
                    Path = $"work5.jpg"
                },
                new StorageFile
                {
                    Id = 6,
                    Path = $"work6.jpg"
                }
            };

            builder.HasData(files);

            builder.ToTable("StorageFiles");
        }
    }
}