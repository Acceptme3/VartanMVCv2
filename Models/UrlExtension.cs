using Microsoft.AspNetCore.Mvc;

namespace VartanMVCv2.Models
{
    public static class UrlExtension
    {
        public static string? Action(this IUrlHelper urlHelper, string? action, string? controller, string? area)
        {
            string? url = $"/{area}/{controller}/{action}";
            Serilog.Log.Information($"Строка пришедшая в метод UrlExtantion {url}");
            return url;
        }

        public static string? Action(this IUrlHelper urlHelper, string? action, string? controller, string? area, object TModel)
        {
            string? url = $"/{area}/{controller}/{action}";
            Serilog.Log.Information($"Строка пришедшая в метод UrlExtantion {url}");
            return url;
        }

    }
}
