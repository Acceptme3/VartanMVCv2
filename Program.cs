using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Repositories.Abstract;
using VartanMVCv2.Domain.Repositories.EntityFramework;
using VartanMVCv2.Services;
using VartanMVCv2.ViewModels;
using VartanMVCv2.Domain.Entities;
using Serilog;
using VartanMVCv2.Models;
using Recaptcha.Web.Configuration;
using System.Configuration;
using VartanMVCv2.Middleware;

namespace VartanMVCv2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*//������������ ������ */
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateBootstrapLogger();
            try
            {
                Log.Debug("Web-���������� ��������.");
                var builder = WebApplication.CreateBuilder(args);
                builder.Host.UseSerilog();
                // add services
                //����������� Identity 
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
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/Accesdenied";
                    options.SlidingExpiration = true;
                });
                // ������������� ����������
                builder.Services.AddAuthorization(x =>
                {
                    x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
                });
                builder.Services.AddControllersWithViews(x =>
                {
                    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
                }).AddSessionStateTempDataProvider();
                //������������ ���������������� json � �������
                builder.Configuration.AddJsonFile("appsettings.json");
                builder.Configuration.AddJsonFile("metasettings.json");
                Config config = new Config();
                RecaptchaConfigurationManager.SetConfiguration(configuration);
                //CaptchaConfiguration captchaConfiguration = new CaptchaConfiguration();
                builder.Configuration.Bind("Project", config);
                // builder.Configuration.Bind("Captcha", captchaConfiguration);
                
                //���������� ����� ��������� ���� ������
                builder.Services.AddDbContext<AplicationDBContext>(option => option.UseLazyLoadingProxies().UseSqlServer(Config.ConnectionString));
                //���������� ������ ���������� � �������� ��������
                builder.Services.AddMemoryCache();
                builder.Services.AddDistributedMemoryCache();
                builder.Services.AddSession(options =>
                {
                    options.Cookie.Name = "CurrentViewName";
                    options.IdleTimeout = TimeSpan.FromMinutes(30);
                    options.Cookie.IsEssential = true;
                });
                builder.Services.AddHttpContextAccessor();
                builder.Services.AddSignalR();
                builder.Services.AddScoped<CurrentViewContext>();
                builder.Services.AddScoped<Modelinitializer>();
                builder.Services.AddTransient<IEntityRepository<WorkServices>, EFEntitiesRepository<WorkServices>>();
                builder.Services.AddTransient<IEntityRepository<WorksCategory>, EFEntitiesRepository<WorksCategory>>();
                builder.Services.AddTransient<IEntityRepository<Work>, EFEntitiesRepository<Work>>();
                builder.Services.AddTransient<IEntityRepository<CompletedProject>, EFEntitiesRepository<CompletedProject>>();
                builder.Services.AddTransient<IEntityRepository<CompletedProjectPhoto>, EFEntitiesRepository<CompletedProjectPhoto>>();
                builder.Services.AddTransient<IEntityRepository<Feedback>, EFEntitiesRepository<Feedback>>();
                builder.Services.AddTransient<IClientRepository, ClientRepository>();
                builder.Services.AddTransient<DataManager>();
                builder.Services.AddTransient<IndexViewModel>();
                builder.Services.AddTransient<ClientViewModel>();
                builder.Services.AddTransient<SortingManager>();

                var app = builder.Build();

                // app 
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                if (app.Environment.IsProduction())
                {
                    //app.UseMiddleware<ExceptionHandlingMiddleware>();
                }

                app.UseHttpsRedirection();

                app.UseStaticFiles();

                app.UseSerilogRequestLogging();

                app.UseSession();

                app.UseRouting();

                app.UseCookiePolicy();

                app.UseAuthentication();

                app.UseAuthorization();
                
                //�������� ��� SignalR 
                app.MapHub<ProgressHub>("/progressHub");

                //�������� ���������� �������
                app.MapAreaControllerRoute(
                   name: "admin",
                   areaName: "Admin",
                   pattern: "admin/{controller}/{action}/{id?}",
                   defaults: new { controller = "Owner", action = "Index" });
                app.MapAreaControllerRoute(
                    name: "client",
                    areaName: "Admin",
                    pattern: "admin/{controller}/{action}/{id?}",
                    defaults: new { controller = "Owner", action = "ClientView" });
                app.MapAreaControllerRoute(
                   name: "commits",
                   areaName: "Admin",
                   pattern: "admin/{controller}/{action}/{id?}",
                   defaults: new { controller = "Owner", action = "Commits" });
                app.MapAreaControllerRoute(
                   name: "feedbackView",
                   areaName: "Admin",
                   pattern: "admin/{controller}/{action}/{id?}",
                   defaults: new { controller = "Owner", action = "FeedbackView" });
                app.MapAreaControllerRoute(
                   name: "selector",
                   areaName: "Admin",
                   pattern: "admin/{controller}/{action}/{id?}",
                   defaults: new { controller = "Owner", action = "Selector" });
                app.MapAreaControllerRoute(
                   name: "defaultErrorPage",
                   areaName: "Admin",
                   pattern: "admin/{controller}/{action}/{id?}",
                   defaults: new { controller = "AdminError", action = "DefaultErrorPage" });
                app.MapAreaControllerRoute(
                   name: "getAddWorkServices",
                   areaName: "Admin",
                   pattern: "admin/{controller}/{action}/{id?}",
                   defaults: new { controller = "Owner", action = "GetAddWorkServices" });
                app.MapAreaControllerRoute(
                   name: "getAddCompletedProject",
                   areaName: "Admin",
                   pattern: "admin/{controller}/{action}/{id?}",
                   defaults: new { controller = "Owner", action = "AddCompletedProject" });

                //��������
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                app.MapControllerRoute(
                    name: "services",
                    pattern: "services/{action}",
                    defaults: new { controller = "Home", action = "Services" });
                app.MapControllerRoute(
                    name: "servicesByID",
                    pattern: "services/{action}/{id?}",
                    defaults: new { controller = "Home", action = "ServicesByID" });
                app.MapControllerRoute(
                    name: "aboutUs",
                    pattern: "about/{action}/{id?}",
                    defaults: new { controller = "Home", action = "About" });
                app.MapControllerRoute(
                    name: "feedback",
                    pattern: "feedback/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Feedback" });
                app.MapControllerRoute(
                    name: "Addfeedback",
                    pattern: "feedback/{action}/{id?}",
                    defaults: new { controller = "Home", action = "AddFeedback" });
                app.MapControllerRoute(
                    name: "contactUs",
                    pattern: "contact/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Contact" });
                app.MapControllerRoute(
                    name: "resourseUsed",
                    pattern: "resourseused/{action}",
                    defaults: new { controller = "Home", action = "ResourseUsed" });
                app.MapControllerRoute(
                    name: "account",
                    pattern: "account/{controller}/{action}/{id?}",
                    defaults: new { controller = "Account", action = "Login" });
                app.MapControllerRoute(
                    name: "download",
                    pattern: "download/{action}/{id?}",
                    defaults: new { controller = "Home", action = "DownloadDocument" });

                app.MapFallbackToController("ErrorPage", "ErrorAplication");

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "��������� ������. ���������� �� ���� ��������.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

    }
}