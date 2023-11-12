using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain.EntitiesConfiguration;
using VartanMVCv2.Services;

namespace VartanMVCv2.Domain
{
    public class AplicationDBContext:IdentityDbContext<IdentityUser>
    {  
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options) { }
        //наши таблицы с услугами
        public DbSet<WorkServices> WorkServices { get; set; }
        public DbSet<WorksCategory> WorksCategory { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<CompletedProject> CompletedProjects { get; set; }
        public DbSet<CompletedProjectPhoto> CompletedProjectPhotos { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Client> DbClients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole{ Id ="0f16741b-318b-4bfe-b01f-a4d058e7d122", Name="admin",NormalizedName = "ADMIN"});

            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "my@email.com",
                NormalizedEmail = "MY@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null!,"superpassword"),
                SecurityStamp=string.Empty
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> {RoleId= "0f16741b-318b-4bfe-b01f-a4d058e7d122" , UserId= "9690ccb5-3b89-457b-a2ee-b7dfab3526a0" });
            builder.Entity<WorkServices>().HasMany(ws => ws.WorksCategories).WithOne(cat => cat.Services).HasForeignKey(ws => ws.WorkServicesID);
            builder.Entity<WorksCategory>().HasMany(cat => cat.Works).WithOne(w => w.WorksCategory);
            //тут мы редактируем таблицы с услугами
            builder.ApplyConfiguration(new WorkServicesConfiguration());
            builder.ApplyConfiguration(new WorksCategoryConfiguration());
            builder.ApplyConfiguration(new WorkConfiguration());

            
        }
    }
}
