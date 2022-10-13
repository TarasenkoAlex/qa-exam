using System;
using System.IO;
using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Protos.RestsAndCities;
using WebApiService.Contracts;

namespace WebApiService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiService", Version = "v1" });

                var xmlFilePath = Path.Combine(AppContext.BaseDirectory, "api.xml");
                if (File.Exists(xmlFilePath))
                {
                    c.IncludeXmlComments(xmlFilePath);
                }
            });
            services.AddGrpcClient<RestsAndCitiesService.RestsAndCitiesServiceClient>(option =>
            {
                option.Address = new Uri("http://localhost:5001");
                
                option.ChannelOptionsActions.Add(channelOption =>
                {
                    channelOption.Credentials = ChannelCredentials.Insecure;
                });
            });
            services.AddScoped<IRestsAndCitiesService, WebApiService.Services.RestsAndCitiesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiService v1"));
        }
    }
}
