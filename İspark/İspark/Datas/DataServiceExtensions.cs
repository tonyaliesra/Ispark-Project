

using İspark.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace İspark.Datas
{
    public static class DataServiceExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 0)),
                    mysqlOptions => mysqlOptions.EnableRetryOnFailure());
            });

            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();

            return services;
        }
    }
}