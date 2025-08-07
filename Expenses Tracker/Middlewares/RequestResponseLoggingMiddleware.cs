using Serilog;

namespace Expenses_Tracker.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;
        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = new LoggerConfiguration()
                .WriteTo.File("Logs/middleware.logs.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Log Request
            _logger.Information("Request: {Method} {Url} {Headers} {Body}",
                context.Request.Method,
                context.Request.Path,
                context.Request.Body,
                context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));
            // Call the next middleware in the pipeline
            await _next(context);
            // Log Response
            _logger.Information("Response: {StatusCode} {Headers}",
                context.Response.StatusCode,
                context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));
        }
    }
}
