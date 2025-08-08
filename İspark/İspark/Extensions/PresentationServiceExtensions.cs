using İspark.Middleware;
using İspark.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace İspark.Extensions
{
    public static class PresentationServiceExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
               
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                        var request = context.HttpContext.Request;

                        logger.LogError("[ERROR{ErrorCode}] Invalid ID format in request. Path: {Path}",
                            ErrorCodes.IdFormatInvalid, request.Path);

                        var errorDetails = new ErrorDetails(StatusCodes.Status400BadRequest, ErrorCodes.IdFormatInvalid);
                        return new BadRequestObjectResult(errorDetails);
                    };
                });
            

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();

                        if (!context.Response.HasStarted)
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                            int errorCode = ErrorCodes.TokenInvalid;
                            string logMessage = "Invalid token or unauthorized access attempt.";

                            if (context.AuthenticateFailure?.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                errorCode = ErrorCodes.TokenExpired;
                                logMessage = "Token has expired.";
                            }

                            logger.LogWarning("[ERROR{ErrorCode}] {LogMessage} Path: {Path}",
                                errorCode, logMessage, context.Request.Path);

                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            var errorDetails = new ErrorDetails(context.Response.StatusCode, errorCode);

                            return context.Response.WriteAsync(errorDetails.ToString());
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        public static WebApplication ConfigurePipeline(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();


            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == 404 && !context.HttpContext.Response.HasStarted)
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError("[ERROR{ErrorCode}] URL not found. Path: {Path}", ErrorCodes.NotFound, context.HttpContext.Request.Path);
                    context.HttpContext.Response.ContentType = "application/json";
                    var errorDetails = new ErrorDetails(StatusCodes.Status404NotFound, ErrorCodes.NotFound);
                    await context.HttpContext.Response.WriteAsync(errorDetails.ToString());
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}