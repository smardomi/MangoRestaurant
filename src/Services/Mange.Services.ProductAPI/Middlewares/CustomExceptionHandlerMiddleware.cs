using System.Net;
using System.Text.Json;
using Mange.Services.ProductAPI.Filters;
using Mange.Services.ProductAPI.Models;

namespace Mange.Services.ProductAPI.Middlewares
{
    public sealed class CustomExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public CustomExceptionHandlerMiddleware(ILogger<CustomExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string message = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            ApiResultStatusCode apiStatusCode = ApiResultStatusCode.ServerError;

            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                if (!_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace,
                    };
                    message = JsonSerializer.Serialize(dic);
                }
                await WriteToResponseAsync();
            }
            async Task WriteToResponseAsync()
            {
                if (context.Response.HasStarted)
                    throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");

                var result = new ApiResult(false, apiStatusCode, message);
                var json = JsonSerializer.Serialize(result);

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }
    }
}