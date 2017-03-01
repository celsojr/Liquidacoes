namespace Liquidacoes.Infrastructure.Errors.Handler
{
    using Nancy;
    using System;
    using log4net;
    using Nancy.ErrorHandling;

    public class LogErrorHandler : IStatusCodeHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LogErrorHandler));

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            if (context.Items.TryGetValue(NancyEngine.ERROR_EXCEPTION, out object errorObject))
            {
                var ex = (errorObject as Exception)?.GetBaseException();
                log.Error("Unhandled error", ex);
            }
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
            => statusCode == Nancy.HttpStatusCode.InternalServerError;
    }
}