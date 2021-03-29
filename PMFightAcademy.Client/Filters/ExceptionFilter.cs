using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace PMFightAcademy.Client.Filters
{
    /// <summary>
    /// Middleware for Exception processing
    /// </summary>
    public class ExceptionFilter : IAsyncExceptionFilter
    {
#pragma warning disable CS1591
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            var json = JsonSerializer.Serialize(new
            {
                message = "Something went wrong. Please, try again later."
            });

            context.Result = new ContentResult
            {
                Content = json
            };

            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
#pragma warning restore CS1591
}
