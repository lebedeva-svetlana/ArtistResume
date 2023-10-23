using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resume.Models
{
    public class BiographyConfiguration : IEntityTypeConfiguration<Biography>
    {
        public void Configure(EntityTypeBuilder<Biography> builder)
        {
            builder
                .HasKey(e => e.Id)
                .HasName("PK_Biography");

            builder.Property(e => e.Markdown)
                .IsRequired();

            List<Biography> biographies = new()
            {
                new Biography
                {
                    Id = 1,
                    Markdown = "Ви́ктор Миха́йлович Васнецо́в (3 мая 1848, Лопьял, Уржумский уезд, Вятская губерния, Российская империя — 23 июля 1926, Москва, СССР) — русский художник-живописец и архитектор, мастер исторической и фольклорной живописи.\r\n\r\nВаснецов является основоположником «неорусского стиля», преобразованного из исторического жанра и романтических тенденций, связанных с фольклором и символизмом. Творчество художника сыграло важную роль в развитии российского изобразительного искусства от эпохи передвижничества к стилю модерн. Действительный статский советник. Младший брат — художник Аполлинарий Васнецов."
                }
            };

            builder.HasData(biographies);

            builder.ToTable("Biographies");
        }
    }
}