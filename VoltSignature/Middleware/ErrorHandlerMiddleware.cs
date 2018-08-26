using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VoltSignature.Common.Exceptions;
using VoltSignature.Model.Settings;

namespace VoltSignature.UI.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,IOptions<LoggerSetting> logSetting, ILoggerFactory loggerFactory)
        {
            this.next = next;
            _logger = loggerFactory.CreateLogger(logSetting.Value.LoggerType);
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private  Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            string message = exception.Message; 
            if (exception is SignatureException) 
                code = ((SignatureException)exception).StatusCode;      
            var result = JsonConvert.SerializeObject(new { error = exception.Message, code, stackTrace=exception.StackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            _logger.LogError(exception.Message,code);
            return context.Response.WriteAsync(result);
        }
    }
}
