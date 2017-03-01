namespace Liquidacoes.Bootstrapping
{
    using Nancy;
    // using log4net;
    using Nancy.TinyIoc;
    using Nancy.Bootstrapper;
    using Nancy.Responses.Negotiation;
    using Liquidacoes.Infrastructure.Settings;
    using Liquidacoes.Infrastructure.Errors.Handler;
    using Liquidacoes.Infrastructure.Errors.Specification.Errors;

    public class LiquidacoesBootstrapper : DefaultNancyBootstrapper
    {
        private readonly IApplicationSettings appSettings;

        public LiquidacoesBootstrapper()
        {
        }

        public LiquidacoesBootstrapper(IApplicationSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<IApplicationSettings>(appSettings);
            //container.Register<ILog>(LogManager.GetLogger(typeof(LiquidacoesBootstrapper)));
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            CustomErrorHandler.Enable(pipelines, container.Resolve<IResponseNegotiator>(), new GeneralServiceError());
        }

    }
}