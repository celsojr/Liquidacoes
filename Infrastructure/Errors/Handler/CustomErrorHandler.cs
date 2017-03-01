namespace Liquidacoes.Infrastructure.Errors.Handler
{
    using Nancy;
    using System;
    using log4net;
    using Nancy.Bootstrapper;
    using Nancy.Responses.Negotiation;
    using Liquidacoes.Infrastructure.Errors.Model;
    using Liquidacoes.Infrastructure.Errors.Extensions;
    using Liquidacoes.Infrastructure.Errors.Exceptions;

    public static class CustomErrorHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CustomErrorHandler));

        public static void Enable(IPipelines pipelines, IResponseNegotiator responseNegotiator, HttpServiceError defaultError)
        {
            if (pipelines == null)
            {
                throw new ArgumentNullException(nameof(pipelines));
            }

            if (responseNegotiator == null)
            {
                throw new ArgumentNullException(nameof(responseNegotiator));
            }

            if (defaultError == null)
            {
                throw new ArgumentNullException(nameof(defaultError));
            }

            pipelines.OnError += (context, exception) => HandleException(context, exception, responseNegotiator, defaultError);
        }

        private static void LogException(NancyContext context, Exception exception)
        {
            if (log.IsErrorEnabled)
            {
                log.ErrorFormat("An exception occured during processing a request. (Exception={0}).", exception);
            }
        }

        private static Response HandleException(NancyContext context, Exception exception, IResponseNegotiator responseNegotiator, HttpServiceError defaultError)
        {
            LogException(context, exception);

            return CreateNegotiatedResponse(context, responseNegotiator, exception, defaultError);
        }

        private static Response CreateNegotiatedResponse(NancyContext context, IResponseNegotiator responseNegotiator, Exception exception, HttpServiceError defaultError)
        {
            HttpServiceError httpServiceError = ExtractFromException(exception, defaultError);

            Negotiator negotiator = new Negotiator(context)
                .WithServiceError(httpServiceError);

            return responseNegotiator.NegotiateResponse(negotiator, context);
        }

        private static HttpServiceError ExtractFromException(Exception exception, HttpServiceError defaultValue)
            => (exception as HttpServiceErrorException)?.HttpServiceError ?? defaultValue;
        
        
    }
}
