using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Domain.EntitiesConfiguration
{
    public class WorksListConfiguration : IEntityTypeConfiguration<WorksList>
    {
        public void Configure(EntityTypeBuilder<WorksList> builder)
        {
            builder.Property(x => x.Title).HasColumnName("Название категории");
        }
    }
}
