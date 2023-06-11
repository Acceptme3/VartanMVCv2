using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain.EntitiesConfiguration
{
    public class WorksNameConfiguration : IEntityTypeConfiguration<WorksName>
    {
        public void Configure(EntityTypeBuilder<WorksName> builder)
        {
            builder.Property(x => x.Title).HasColumnName("Наименование работы");
        }
    }
}
