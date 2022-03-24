using Microsoft.Extensions.DependencyInjection;
using MandradeFrameworks.Mensagens.Configuration;
using Microsoft.AspNetCore.Mvc;
using MandradeFrameworks.Retornos.Configuration;
using Microsoft.Extensions.Configuration;
using MandradeFrameworks.Logs.Models;
using MandradeFrameworks.Autenticacao.Configuration;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using static MandradeFrameworks.Logs.Configuration.LogsConfiguration;
using static Infrastructure.Configuration.RepositoryDependencyInjection;
using static Application.Configuration.ApplicationDependencyInjection;

namespace Api.Configuration
{
    public static class ApiConfiguration
    {
        private const string NOME_API = "Scaffold API";
        private const string VERSAO_API = "v1";
        private const string TABELA_LOGS = "Logs_Scaffold";

        public static void AdicionarDependencyInjection(this IServiceCollection services)
        {
            services.AdicionarRepositorios();
            services.AdicionarServicos();
        }

        public static void AdicionarPacotesFramework(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionStringLogs = configuration.GetConnectionString("Logs");
            var configuracoesLogs = new SQLLogsConfiguration(connectionStringLogs, TABELA_LOGS);

            services.AdicionarMensageria();
            services.AdicionarAutenticacao();
            AdicionarLogsSQL(configuracoesLogs);
        }

        public static void AdicionarMiddlewares(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(cnf =>
            {
                cnf.SwaggerDoc(VERSAO_API, new OpenApiInfo { Version = VERSAO_API, Title = NOME_API });
                cnf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Header autenticacao via Json Web Tokens (JWT). insira abaixo o seu token da seguinte forma: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                cnf.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                        {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                    }
                });
            });
        }

        public static void AdicionarMiddlewaresAplicacao(this IApplicationBuilder application)
        {
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            application.UseSwagger();
            application.UseSwaggerUI();
        }

        public static void AdicionarPacotesFramework(this MvcOptions options)
        {
            options.AdicionarConfiguracoes();
        }
    }
}
