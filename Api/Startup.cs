using Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string NOME_API = "Scaffold API";
        private const string VERSAO_API = "v1";
        private const string NOME_POLITICA_CORS = "Autorização Front-end";
        private string[] ORIGENS_CORS = new string[] { "http://localhost:4200", "https://localhost:4200" };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => {
                options.AddPolicy(
                    NOME_POLITICA_CORS,
                    builder => builder
                        .WithOrigins(ORIGENS_CORS)
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                );
            });

            services.AdicionarPacotesFramework(Configuration);

            services.AddSwaggerGen(options =>
                options.SwaggerDoc(VERSAO_API, new OpenApiInfo
                {
                    Version = VERSAO_API,
                    Title = NOME_API
                })
            );

            services.AddMvc(options => options.AdicionarPacotesFramework());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string>() { "index.html" } });
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
