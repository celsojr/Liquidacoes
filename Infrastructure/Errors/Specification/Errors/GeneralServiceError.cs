namespace Liquidacoes.Infrastructure.Errors.Specification.Errors
{
    using Liquidacoes.Infrastructure.Errors.Enums;
    using Liquidacoes.Infrastructure.Errors.Model;

    public class GeneralServiceError : HttpServiceError
    {
        public GeneralServiceError()
        {
            HttpStatusCode = Nancy.HttpStatusCode.BadRequest;
            ServiceError = new ServiceErrorModel
            {
                Code = ServiceErrorEnum.GeneralError,
                Details = "An Error occured."
            };
        }
    }
}
