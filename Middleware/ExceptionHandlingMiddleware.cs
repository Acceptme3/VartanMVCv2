using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using VartanMVCv2.Services.Dto;
using VartanMVCv2.ViewModels;

namespace VartanMVCv2.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            
            catch (Exception exception)
            {
                HandleExceptionAsync(httpContext, exception, HttpStatusCode.NotFound, "ERROR TEst");      
            }
        }

        public void HandleExceptionAsync(HttpContext httpContext, Exception exception, HttpStatusCode httpStatusCode, string message)
        {
            _logger.LogError($"Ошибка! Сообщение: {exception.Message}, Стек:{exception.StackTrace}");

            HttpResponse response = httpContext.Response;
            response.Clear();
            response.ContentType = "text/html";
            response.StatusCode = (int) httpStatusCode;

             response.Redirect("/ErrorAplication/ErrorPage");
        }
    }
}
