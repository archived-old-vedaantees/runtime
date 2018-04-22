using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vedaantees.Framework.Providers;
using Vedaantees.Framework.Providers.DependencyInjection;
using Vedaantees.Framework.Configurations;
using Autofac.Extensions.DependencyInjection;
using System;
using IdentityServer4.Validation;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vedaantees.Hosts.SingleSignOn.Stores;
using Serilog;
using System.Collections.Generic;

namespace Vedaantees.Hosts.SingleSignOn
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        internal IConfigurationRoot Configuration { get; set; }
        public Autofac.IContainer ApplicationContainer { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            Configuration = ProviderBootstrapper.BuildConfiguration(env);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new AutofacContainerBuilder();
            var providerConfiguration = Configuration.GetSection("Vedaantees").Get<ProviderConfiguration>();
            providerConfiguration.ModulesFolder = string.Empty;

            var bootstrapper = new ProviderBootstrapper(builder, providerConfiguration);
            bootstrapper.Load(_env);

            services.AddMvc().AddRazorOptions(options =>
                                              {
                                                    options.ViewLocationFormats.Clear();
                                                    options.ViewLocationFormats.Add("/Presentation/{0}.cshtml");
                                              });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                    .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                    .AddInMemoryClients(IdentityServerConfiguration.GetClients(Configuration.GetSection("Clients").Get<List<string>>()));
            
            services.AddTransient<IResourceOwnerPasswordValidator, UserStore>()
                    .AddTransient<IProfileService, UserStore>();

            builder.RegisterInstance<IConfiguration>(Configuration);
            builder.Builder.Populate(services);
            ApplicationContainer = builder.Build();
            bootstrapper.InitializeFramework(ApplicationContainer);
            return new AutofacServiceProvider(ApplicationContainer);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
                app.UseExceptionHandler("/Error");
            
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Account}/{action=SignIn}"); });
            
        }
    }

    #region " Program "
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var hostProgram = new HostProgram("http://localhost:6010");
            hostProgram.Main<Startup>();
        }
    }
    #endregion
}
