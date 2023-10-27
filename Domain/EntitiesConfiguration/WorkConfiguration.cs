using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain.EntitiesConfiguration
{
    public class WorkConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.Property(x => x.Title).HasColumnName("Наименование работы");
        }
    }
}
