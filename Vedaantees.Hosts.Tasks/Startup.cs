using System;
using Autofac.Extensions.DependencyInjection;
using Vedaantees.Framework.Configurations;
using Vedaantees.Framework.Providers;
using Vedaantees.Framework.Providers.DependencyInjection;
using Vedaantees.Framework.Providers.Tasks;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IContainer = Autofac.IContainer;

namespace Vedaantees.Hosts.Tasks
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        internal IConfigurationRoot Configuration { get; set; }
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

            services.AddMvc();
            services.AddHangfire(x => x.UsePostgreSqlStorage(providerConfiguration.SqlStore.ConnectionString));

            builder.RegisterInstance<IConfiguration>(Configuration);
            builder.Builder.Populate(services);
            ApplicationContainer = builder.Build();
            bootstrapper.InitializeFramework(ApplicationContainer);

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, TasksManager tasksManager)
        {
            loggerFactory.AddConsole();
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            tasksManager.Configure();
        }
    }
    
    #region " Program "
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var program = new HostProgram("http://localhost:6003");
            program.Main<Startup>();
        }
    }
    #endregion
}