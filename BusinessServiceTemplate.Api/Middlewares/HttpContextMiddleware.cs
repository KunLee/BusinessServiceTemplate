using BusinessServiceTemplate.Api.Common;
using Serilog.Context;
using System.Diagnostics;

namespace BusinessServiceTemplate.Api.Middlewares
{
    /// <summary>
    /// Add the current http request and response to the log context.
    /// </summary>
    public class HttpContextMiddleware
    {
        private const string HttpRequestPropertyName = "HttpRequest";
        private const string HttpResponsePropertyName = "HttpResponse";

        private readonly ILogger<HttpContextMiddleware> _logger;
        private readonly RequestDelegate _next;

        public HttpContextMiddleware(RequestDelegate next, ILogger<HttpContextMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var httpRequestInfo = await HttpContextInfoHelper.GetHttpRequestInfoAsync(httpContext);

            // Push the user name into the log context so that it is included in all log entries

            using (LogContext.PushProperty(HttpRequestPropertyName, httpRequestInfo, true))
            {
                var stopwatch = Stopwatch.StartNew();

                await _next(httpContext);

                stopwatch.Stop();

                using (LogContext.PushProperty(HttpResponsePropertyName, httpContext.Response.StatusCode, true))
                {
                    _logger.LogInformation("{duration}ms", (int)Math.Ceiling(stopwatch.Elapsed.TotalMilliseconds));
                }
            }
        }
    }
}
