namespace Liquidacoes
{
    using Nancy.Owin;
    using Liquidacoes.Bootstrapping;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Liquidacoes.Infrastructure.Settings;

    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .SetBasePath(env.ContentRootPath);

            config = builder.Build();
        }

        public void Configure(IApplicationBuilder app)
        {
            var appSetting = new ApplicationSettings();
            ConfigurationBinder.Bind(config, appSetting);

            app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new LiquidacoesBootstrapper(appSetting)));
        }
    }
}