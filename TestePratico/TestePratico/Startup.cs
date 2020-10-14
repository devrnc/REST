using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using TestePratico.Model.Context;
using TestePratico.Business;
using TestePratico.Business.Implementattions;
using TestePratico.Repository.Implementattion;
using Swashbuckle.AspNetCore.Swagger;


namespace TestePratico
{
    public class Startup
    {
        private readonly ILogger logger;
        public IConfiguration configuration { get; }
        public IHostingEnvironment environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            this.configuration = configuration;
            this.environment = environment;
            this.logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Connection to database
            string connectionString = this.configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            //Adding Migrations Support
            if (this.environment.IsDevelopment())
            {
                try
                {
                    MySql.Data.MySqlClient.MySqlConnection evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    Evolve.Evolve evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => this.logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();

                }
                catch (Exception ex)
                {
                    this.logger.LogCritical("Database migration failed.", ex);
                    throw;
                }
            }

            //Content negociation - Support to XML and JSON
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
            })
            .AddXmlSerializerFormatters();

            //Versioning
            services.AddApiVersioning(option => option.ReportApiVersions = true);

            //Add Swagger Service
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "REST API With ASP.NET Core 2.0",
                        Version = "V1"
                    });

            });

            //Dependency Injection
            services.AddScoped<IPaymentsBusiness, PaymentsBusinessImpl>();
            services.AddScoped<IPaymentsRepository, PaymentsRepositoryImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Enable Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            //Starting our API in Swagger page
            RewriteOptions option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            //Adding map routing
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}");
            });
        }

    }
}
