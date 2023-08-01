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
        public DbSet<WorksList> WorksList { get; set; }
        public DbSet<WorksName> WorksName { get; set; }
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
            //тут мы редактируем таблицы с услугами
            builder.ApplyConfiguration(new WorkServicesConfiguration());
            builder.ApplyConfiguration(new WorksListConfiguration());
            builder.ApplyConfiguration(new WorksNameConfiguration());

            builder.Entity<WorkServices>().HasData(new WorkServices { ID = 1, Title = "Дизайн-проект", Description = "Разработка индивидуального дизайн-проекта.", TitleImagePath = "/images/img-2.png" });

            builder.Entity<CompletedProject>().HasData(new CompletedProject { ID = 1, Title = "Ремонт ванной 6 кв.м", TitleImagePath = "/images/img-5.png" });
            builder.Entity<CompletedProject>().HasData(new CompletedProject { ID = 2, Title = "Гостинная 18 кв.м", TitleImagePath = "/images/img-4.png" });

            builder.Entity<Feedback>().HasData(new Feedback { ID = 1, TitleImagePath = "/images/img-7.png", FeedbackClientName = "Антон", FeedbackText = "Все просто шикаорно! Парни красавцы" });
            builder.Entity<Feedback>().HasData(new Feedback { ID = 2, TitleImagePath = "/images/img-8.png", FeedbackClientName = "Асламбек", FeedbackText = "Аллах Свидетель лучший ремонт прихожей в моей жизни" });
        }
    }
}
