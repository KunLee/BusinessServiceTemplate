using BusinessServiceTemplate.Api.Common;
using BusinessServiceTemplate.Api.Settings;
using BusinessServiceTemplate.Shared.Common;
using LoggingService.Client.LoggingService;
using LoggingService.Client.LoggingService.Models;
using LoggingService.Client.References;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Reflection.Metadata;

namespace BusinessServiceTemplate.Api.Middlewares
{
    /// <summary>
    /// The exception handlling middleware which is responsible for catching and handling exceptions that occur in the Web API pipeline
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly ILoggingServiceClient _loggingService;
        private readonly LoggingServiceSettings _loggingSetting;

        public ExceptionHandlingMiddleware
            (RequestDelegate requestDelegate, 
                ILogger<ExceptionHandlingMiddleware> logger, 
                ILoggingServiceClient loggingService, IOptions<LoggingServiceSettings>  loggingSetting)
        {
            _next = requestDelegate;
            _logger = logger;
            _loggingService = loggingService;
            _loggingSetting = loggingSetting.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private async Task HandleException(HttpContext context, Exception ex)
        {
            Guid errorId = Guid.NewGuid();

            string codeMsg = ConstantStrings.INTERNAL_SYSTEM_ERROR_MESSAGE;

            Type exType = ex.GetType();

            if (!exType.Name.Equals(nameof(Exception)))
            {
                codeMsg = ex.Message;
            }

            var errorMessageObject = new
            {
                Code = codeMsg,
                ErrorId = errorId,
                //Exception = ex
            };

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);

            if (_loggingSetting.RemoteLogging)
            {
                await SendRemoteLogging(ex, errorMessage, context);
            }

            _logger.LogError(exception: ex, message: "{@DataObject}", errorMessageObject);

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(errorMessage);
        }

        private async Task SendRemoteLogging(Exception ex, string msg, HttpContext context)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var httpRequestInfo = await HttpContextInfoHelper.GetHttpRequestInfoAsync(context);

            var logRequest = new GetLogRequest
            {
                Entries = new List<GetLogEntry>
                {
                    new GetLogEntry {
                        LogLevel = LogSeverity.ERROR,
                        EnvironmentName= environment,
                        HostName=  _loggingSetting.HostName,
                        InstanceId = _loggingSetting.InstanceId,
                        LogSource =  typeof(ExceptionHandlingMiddleware).ToString(),
                        AppName =  _loggingSetting.AppName,
                        Message = msg,
                        ContextData = JsonConvert.SerializeObject(httpRequestInfo),
                        StackTrace = ex.StackTrace.Length > 5000 ? ex.StackTrace?.Substring(0, 4990) : ex.StackTrace
                    },
                }
            };
            await _loggingService.Send(logRequest);
        }
    }
}
