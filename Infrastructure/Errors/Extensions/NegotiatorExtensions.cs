namespace Liquidacoes.Infrastructure.Errors.Extensions
{
    using Nancy;
    using Nancy.Responses.Negotiation;
    using Liquidacoes.Infrastructure.Errors.Model;

    public static class NegotiatorExtensions
    {
        public static Negotiator WithServiceError(this Negotiator negotiator, HttpServiceError httpServiceError)
        {
            return negotiator
                .WithStatusCode(httpServiceError.HttpStatusCode)
                .WithModel(httpServiceError.ServiceError);
        }
    }
}
