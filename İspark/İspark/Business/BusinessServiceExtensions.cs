// Konum: İspark.Business/BusinessServiceExtensions.cs

using İspark.Services;
using Microsoft.Extensions.DependencyInjection;

namespace İspark.Business
{
    public static class BusinessServiceExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<LoggerService>();
            services.AddScoped<INewsService, NewsService>();

            return services;
        }
    }
}