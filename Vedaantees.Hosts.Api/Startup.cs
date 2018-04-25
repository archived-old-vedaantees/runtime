using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Vedaantees.Framework.Configurations;
using Vedaantees.Framework.Providers;
using Vedaantees.Framework.Providers.Communications.Rest;
using Vedaantees.Framework.Providers.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IContainer = Autofac.IContainer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Vedaantees.Hosts.Api
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        internal IConfigurationRoot Configuration { get; set; }
        public IContainer ApplicationContainer { get; private set; }
        
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            Configuration = ProviderBootstrapper.BuildConfiguration(env);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new AutofacContainerBuilder();
            var providerConfiguration = Configuration.GetSection("Vedaantees").Get<ProviderConfiguration>();
            var bootstrapper = new ProviderBootstrapper(builder, providerConfiguration);
            bootstrapper.Load(_env);
            LoadApiClientRegistrations(builder, bootstrapper.GetAssemblies);

            services.AddMvcCore(config => {
                //Enable this for security.
                //var policy = new AuthorizationPolicyBuilder()
                //                .RequireAuthenticatedUser()
                //                .Build();
                //config.Filters.Add(new AuthorizeFilter(policy));
            })
                    .AddAuthorization()
                    .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = providerConfiguration.SingleSignOnServer;
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "axelius-api";
                        options.ApiSecret = "guess-thi$-not-di55icult-resource";
                    });

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(string.Join(',',Configuration.GetSection("Clients").Get<List<string>>()))
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.RegisterInstance(Configuration);
            builder.Builder.Populate(services);
            ApplicationContainer = builder.Build();
            bootstrapper.InitializeFramework(ApplicationContainer);
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ProviderConfiguration configuration)
        {
            app.UseCors("default");
            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseMvc(config =>
                       {
                           config.MapRoute("ping", "{controller=Singularity}/{action=Ping}");
                       });

        }

        private void LoadApiClientRegistrations(AutofacContainerBuilder builder, List<Assembly> assemblies)
        {
            var registrations = new ApiClientRegistrations();

            foreach (var assembly in assemblies)
                foreach (var type in assembly.GetTypes())
                {
                    var api = (EnableApiAttribute)type.GetCustomAttribute(typeof(EnableApiAttribute));

                    if (api == null)
                        continue;
                    registrations.Add(new ApiClientRegistration { Route = api.Route, Type = type });
                }

            builder.RegisterInstance(registrations);
        }
    }

    #region " Program "
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var hostProgram = new HostProgram("http://localhost:6001");
            hostProgram.Main<Startup>();
        }
    }
    #endregion
}