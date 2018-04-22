using System;
using Autofac.Extensions.DependencyInjection;
using Vedaantees.Framework.Configurations;
using Vedaantees.Framework.Providers;
using Vedaantees.Framework.Providers.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IContainer = Autofac.IContainer;

namespace Vedaantees.Hosts.ServiceBus
{
    public class Startup
    {
        internal IConfigurationRoot Configuration { get; set; }
        private readonly IHostingEnvironment _env;
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = ProviderBootstrapper.BuildConfiguration(env);
            _env = env;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new AutofacContainerBuilder();
            var providerConfiguration = Configuration.GetSection("Vedaantees").Get<ProviderConfiguration>();
            var bootstrapper = new ProviderBootstrapper(builder, providerConfiguration);
            bootstrapper.Load(_env);

            
            builder.Builder.Populate(services);
            ApplicationContainer = builder.Build();
            bootstrapper.InitializeFramework(ApplicationContainer);
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
        }
    }

    #region " Program "
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var hostProgram = new HostProgram("http://localhost:6004");
            hostProgram.Main<Startup>();
        }
    }
    #endregion
}