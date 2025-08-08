using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace İspark.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next; 
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;
            var requestLog = new StringBuilder();
            requestLog.Append($"[REQUEST] {request.Method} {request.Path}{request.QueryString}");

            if (request.Method == "POST" || request.Method == "PUT" || request.Method == "PATCH")
            {
                request.EnableBuffering();
                var body = await new StreamReader(request.Body).ReadToEndAsync();
                request.Body.Position = 0;
                body = Regex.Replace(body, "(\"password\"\\s*:\\s*\").*?(\")", "$1***$2", RegexOptions.IgnoreCase);
                body = body.Replace("\r", "").Replace("\n", "").Replace("  ", " ");
                if (body.Length > 1000) body = body.Substring(0, 1000) + "...(shortened.)";
                requestLog.Append($" | Body: {body}");
            }
            _logger.LogInformation(requestLog.ToString());

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context); 

            var responseText = await ReadResponseBody(context.Response);
            stopwatch.Stop();
            responseText = Regex.Replace(responseText, "(\"token\"\\s*:\\s*\").*?(\")", "$1...masked...$2", RegexOptions.IgnoreCase);
            if (context.Response.ContentType?.Contains("text/html") == true) responseText = "[HTML content is hidden.]";
            else if (responseText.Length > 1000) responseText = responseText.Substring(0, 1000) + "...(shortened.)";

            _logger.LogInformation($"[RESPONSE] {context.Response.StatusCode} ({stopwatch.ElapsedMilliseconds}ms) | Path: {request.Method} {request.Path}{request.QueryString}");
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}