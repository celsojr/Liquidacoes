namespace Liquidacoes.Infrastructure.Errors.Specification.Errors
{
    using Nancy.Validation;
    using Liquidacoes.Infrastructure.Errors.Enums;
    using Liquidacoes.Infrastructure.Errors.Model;
    using Liquidacoes.Infrastructure.Validation.Nancy;

    public class ValidationServiceError : HttpServiceError
    {
        public ValidationServiceError(ModelValidationResult modelValidationResult)
        {
            HttpStatusCode = Nancy.HttpStatusCode.BadRequest;
            ServiceError = new ServiceErrorModel
            {
                Code = ServiceErrorEnum.ValidationError,
                Details = modelValidationResult.GetDetailedErrorMessage()
            };
        }
    }
}
