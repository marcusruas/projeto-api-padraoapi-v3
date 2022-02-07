using Microsoft.Extensions.DependencyInjection;
using MandradeFrameworks.Mensagens.Configuration;

namespace Api.Configuration
{
    public static class DependencyInjection
    {
        public static void AdicionarPacotesFramework(this IServiceCollection services)
        {
            services.AdicionarMensageria();
        }
    }
}
