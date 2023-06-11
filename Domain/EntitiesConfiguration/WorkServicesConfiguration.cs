using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain.EntitiesConfiguration
{
    public class WorkServicesConfiguration : IEntityTypeConfiguration<WorkServices>
    {
        public void Configure(EntityTypeBuilder<WorkServices> builder)
        {
            builder.Property(x => x.Title).HasColumnName("Заголовок услуги");
            builder.Property(x => x.Description).HasColumnName("Описание услуги");
            builder.Property(x => x.TitleImagePath).HasColumnName("Путь к файлу изображения");
        }
    }
}
