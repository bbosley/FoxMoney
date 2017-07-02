using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FoxMoney.Server;
using FoxMoney.Server.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Logging;
using FoxMoney.Server.Helpers;
using AutoMapper;
using FoxMoney.Server.Repositories.Abstract;
using FoxMoney.Server.Repositories;
using Hangfire;
using Hangfire.PostgreSql;
using FoxMoney.Server.Services;
using Microsoft.AspNetCore.Identity;
using FoxMoney.Server.Entities;
using System.Collections.Generic;
using System;

namespace FoxMoney
{
    public class Startup
    {
        private IHostingEnvironment _hostingEnv;
        
        public Startup(IHostingEnvironment env)
        {
            _hostingEnv = env;

            //Helpers.SetupSerilog();

            var builder = new ConfigurationBuilder()
                           .SetBasePath(env.ContentRootPath)
                           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                           .AddEnvironmentVariables();
            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = Helpers.DefaultMimeTypes;
            });

            services.AddCustomDbContext();

            services.AddCustomIdentity();

            services.AddCustomOpenIddict();
            
            services.AddMemoryCache();

            services.RegisterCustomServices();

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddCustomizedMvc();

            // Node services are to execute any arbitrary nodejs code from .net
            services.AddNodeServices();

            services.AddAutoMapper();

            services.AddTransient(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));
            services.AddTransient<PortfolioRepository, PortfolioRepository>();
            services.AddTransient<ParcelRepository, ParcelRepository>();

            services.AddSingleton(new BackgroundProcessingService(Configuration));

            services.AddHangfire(config => config.UsePostgreSqlStorage(Configuration["Data:PostgresqlConnectionString"]));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AspNetCoreSpa", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.AddDevMiddlewares();

            if (_hostingEnv.IsProduction())
            {
                app.UseResponseCompression();
            }

            app.SetupMigrations();

            app.UseXsrf();

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseOpenIddict();

            // Add a middleware used to validate access
            // tokens and protect the API endpoints.
            app.UseOAuthValidation();

            // Alternatively, you can also use the introspection middleware.
            // Using it is recommended if your resource server is in a
            // different application/separated from the authorization server.
            //
            // app.UseOAuthIntrospection(options => {
            //     options.AutomaticAuthenticate = true;
            //     options.AutomaticChallenge = true;
            //     options.Authority = "http://localhost:54895/";
            //     options.Audiences.Add("resource_server");
            //     options.ClientId = "resource_server";
            //     options.ClientSecret = "875sqd4s5d748z78z7ds1ff8zz8814ff88ed8ea4z4zzd";
            // });

            app.UseOAuthProviders();

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<BackgroundProcessingService>("end_of_day", service => service.UpdateEndOfDayValues(), 
                "40 16 * * 1-5", TimeZoneInfo.Local);

            app.UseMvc(routes =>
            {
                // http://stackoverflow.com/questions/25982095/using-googleoauth2authenticationoptions-got-a-redirect-uri-mismatch-error
                routes.MapRoute(name: "signin-google", template: "signin-google", defaults: new { controller = "Account", action = "ExternalLoginCallback" });

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

    }
}
