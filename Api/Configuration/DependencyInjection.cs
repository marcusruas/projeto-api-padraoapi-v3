using Microsoft.Extensions.DependencyInjection;
using MandradeFrameworks.Mensagens.Configuration;
using Microsoft.AspNetCore.Mvc;
using MandradeFrameworks.Retornos.Configuration;
using Microsoft.Extensions.Configuration;
using MandradeFrameworks.Logs.Configuration;
using MandradeFrameworks.Logs.Models;
using MandradeFrameworks.Autenticacao.Configuration;
using MandradeFrameworks.Tests.Models;
using MandradeFrameworks.Tests.Configuration;

namespace Api.Configuration
{
    public static class DependencyInjection
    {
        public static void AdicionarPacotesFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var configuracoesLogs = configuration.GetSection("SQLConfigurationLogs").Get<SQLLogsConfiguration>();
            var configuracoesTestes = configuration.GetSection("Tests").Get<ConfiguracoesTestes>();

            services.AdicionarMensageria();
            services.AdicionarAutenticacao();
            services.AdicionarTestes(configuracoesTestes);
            LogsConfiguration.AdicionarLogs(configuracoesLogs);
        }

        public static void AdicionarPacotesFramework(this MvcOptions options)
        {
            options.AdicionarConfiguracoes();
        }
    }
}
