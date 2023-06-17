using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Repositories.Abstract;
using VartanMVCv2.Domain.Repositories.EntityFramework;
using VartanMVCv2.Services;
using VartanMVCv2.ViewModels;
using System.Collections.Generic;
using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // add services
            builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
            //сопостовляем конфигурационный json с классом
            builder.Configuration.AddJsonFile("appsettings.json");
            Config config = new Config();
            builder.Configuration.Bind("Project", config);
            //подключаем класс контекста базы данных
            builder.Services.AddDbContext<AplicationDBContext>(x => x.UseSqlServer(Config.ConnectionString));
            //подключаем нужный функционал в качестве сервисов
            builder.Services.AddTransient<IEntityRepository<WorkServices>, EFEntitiesRepository<WorkServices>>();
            builder.Services.AddTransient<IEntityRepository<WorksList>, EFEntitiesRepository<WorksList>>();
            builder.Services.AddTransient<IEntityRepository<WorksName>, EFEntitiesRepository<WorksName>>();
            builder.Services.AddTransient<IEntityRepository<CompletedProject>, EFEntitiesRepository<CompletedProject>>();
            builder.Services.AddTransient<IEntityRepository<CompletedProjectPhoto>, EFEntitiesRepository<CompletedProjectPhoto>>();
            builder.Services.AddTransient<IEntityRepository<Feedback>, EFEntitiesRepository<Feedback>>();
            builder.Services.AddTransient<DataManager>();
            builder.Services.AddTransient<IndexViewModel>();
            //настраиваем Identity 
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = false;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AplicationDBContext>().AddDefaultTokenProviders();
            //autentificatio cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accesdenied";
                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            // app 
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" });
                
            app.MapControllerRoute(
                name: "services",
                pattern: "{action}",
                defaults: new { controller = "Home", action = "Services" });

            app.MapControllerRoute(
                name: "servicesByID",
                pattern: "{action}/{id?}",
                defaults: new { controller = "Home", action = "ServicesByID" });

            app.MapControllerRoute(
                name: "aboutUs",
                pattern: "{action}/{id?}",
                defaults: new { controller = "Home", action = "About" });

            app.MapControllerRoute(
                name: "feedback",
                pattern: "{action}/{id?}",
                defaults: new { controller = "Home", action = "Feedback" });

            app.MapControllerRoute(
                name: "contactUs",
                pattern: "{action}/{id?}",
                defaults: new { controller = "Home", action = "Contact" });

            app.MapControllerRoute(
                name: "resourseUsed",
                pattern: "{action}",
                defaults: new { controller = "Home", action = "ResourseUsed" });


            app.MapFallbackToController("ErrorPage", "ErrorAplication");

            app.Run();
        }
    }
}