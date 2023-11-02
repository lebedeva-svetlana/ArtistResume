using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resume.Models
{
    public class SocialConfiguration : IEntityTypeConfiguration<Social>
    {
        public void Configure(EntityTypeBuilder<Social> builder)
        {
            builder
                .HasKey(e => e.Id)
                .HasName("PK_Social");

            builder.Property(e => e.FontClass)
                .IsRequired();

            List<Social> socials = new()
            {
                new Social
                {
                    Id = 1,
                    FontClass = $"fa fa-flickr"
                },
                new Social
                {
                    Id = 2,
                    FontClass = $"fa fa-behance"
                },
                new Social
                {
                    Id = 3,
                    FontClass = $"fa fa-vk"
                },
                new Social
                {
                    Id = 4,
                    Url = "https://www.youtube.com/watch?v=L9m0C8bppZM",
                    FontClass = $"fa fa-youtube-play"
                },
                new Social
                {
                    Id = 5,
                    Url = $"https://www.tretyakovgallery.ru/for-visitors/museums/dom-muzey-v-vasnetsova/",
                    FontClass = $"fa fa-university"
                },
                new Social
                {
                    Id = 6,
                    FontClass = $"fa fa-camera-retro"
                },
                new Social
                {
                    Id = 7,
                    FontClass = $"fa fa-paint-brush"
                }
            };

            builder.HasData(socials);

            builder.ToTable("Socials");
        }
    }
}