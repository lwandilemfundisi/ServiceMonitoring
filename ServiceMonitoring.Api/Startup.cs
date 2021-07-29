using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Extensions;
using Microservice.Framework.Persistence;
using Microservice.Framework.Persistence.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ServiceMonitoring.Domain;
using ServiceMonitoring.Persistence;
using ServiceMonitoring.Persistence.Extensions;

namespace ServiceMonitoring.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "enableCors",
                    builder =>
                    {
                        builder
                        .WithOrigins("https://localhost:44352")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddLogging(l => l.AddConsole());

            DomainContainer.New(services)
                .ConfigureServiceMonitoringDomain()
                .ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDbContextProvider<ServiceMonitorContext, ServiceMonitorContextProvider>()
                .RegisterServices(sr =>
                {
                    sr.AddTransient<IPersistenceFactory, EntityFrameworkPersistenceFactory<ServiceMonitorContext>>();
                    sr.AddSingleton(rctx => { return Configuration; });
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServiceMonitoring.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServiceMonitoring.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("enableCors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
