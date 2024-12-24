using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OT.Assessment.Infrastructure.Context;

namespace OT.Assessment.Infrastructure
{
    public static class ServiceCollectionExtentions
    {
        public static void AddStorage(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString("DatabaseConnection")));
        }
    }
}
