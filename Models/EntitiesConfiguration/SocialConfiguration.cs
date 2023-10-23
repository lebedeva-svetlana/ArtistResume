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

            builder.Property(e => e.Url)
                .IsRequired();
            builder.Property(e => e.FontClass)
                .IsRequired();

            List<Social> socials = new()
            {
                new Social
                {
                    Id = 1,
                    Url = $"https://www.tretyakovgallery.ru/for-visitors/museums/dom-muzey-v-vasnetsova/",
                    FontClass = $"fa fa-university"
                },
                new Social
                {
                    Id = 2,
                    Url = $"https://w.wiki/7ZST",
                    FontClass = $"fa fa-wikipedia-w"
                }
            };

            builder.HasData(socials);

            builder.ToTable("Socials");
        }
    }
}