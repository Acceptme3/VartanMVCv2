using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain.EntitiesConfiguration
{
    public class WorksCategoryConfiguration : IEntityTypeConfiguration<WorksCategory>
    {
        public void Configure(EntityTypeBuilder<WorksCategory> builder)
        {
            builder.Property(x => x.Title).HasColumnName("Название категории");
        }
    }
}
