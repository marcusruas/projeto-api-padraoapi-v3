using Infrastructure.Area.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class RepositoryDependencyInjection
    {
        public static void AdicionarRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IAreaRepository, AreaRepository>();
        }
    }
}