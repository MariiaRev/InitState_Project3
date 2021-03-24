using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PMFightAcademy.Client.Filters
{
    /// <summary>
    /// Middleware for Exception processing
    /// </summary>
    public class ExceptionFilter : IAsyncExceptionFilter
    {
#pragma warning disable CS1591
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var json = JsonSerializer.Serialize(new
            {
                context.Exception.Message
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
