using Infrastructure.Area.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Area
{
    public static class DependencyInjection
    {
        public static void AdicionarDependencias(this IServiceCollection services)
        {
            services.AddScoped<IAreaRepository, AreaRepository>();
        }
    }
}
