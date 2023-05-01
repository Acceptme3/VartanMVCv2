namespace VartanMVCv2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // add services
            builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();

            var app = builder.Build();

            // app 
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            /*app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "about",
                pattern: "{controller=Home}/{action=About}/{id?}");
            app.MapControllerRoute(
                name: "contact",
                pattern: "{controller=Home}/{action=Contact}/{id?}");
            app.MapControllerRoute(
                name: "shop",
                pattern: "{controller=Home}/{action=Shop}/{id?}");
            app.MapControllerRoute(
                name: "services",
                pattern: "{controller=Home}/{action=Services}/{id?}");
            */
            app.MapControllerRoute(
                name: "default",
                pattern: "{action}",
                defaults: new { controller = "Home", action = "Index" });
                
            app.MapControllerRoute(
                name: "services",
                pattern: "{action}",
                defaults: new { controller = "Home", action = "Services" });

            app.MapControllerRoute(
                name: "aboutUs",
                pattern: "{action}",
                defaults: new { controller = "Home", action = "About" });

            app.MapControllerRoute(
                name: "feedback",
                pattern: "{action}",
                defaults: new { controller = "Home", action = "Feedback" });

            app.MapControllerRoute(
                name: "contactUs",
                pattern: "{action}",
                defaults: new { controller = "Home", action = "Contact" });

            app.MapFallbackToController("ErrorPage", "ErrorAplication");

            app.Run();
        }
    }
}