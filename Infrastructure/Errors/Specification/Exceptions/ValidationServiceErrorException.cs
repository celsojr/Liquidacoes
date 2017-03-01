namespace Liquidacoes.Infrastructure.Specification.Exceptions
{
    using System;
    using Nancy.Validation;
    using Liquidacoes.Infrastructure.Errors.Exceptions;
    using Liquidacoes.Infrastructure.Errors.Specification.Errors;

    public class ValidationServiceErrorException : HttpServiceErrorException
    {
        public ValidationServiceErrorException(ModelValidationResult modelValidationResult)
            : base(new ValidationServiceError(modelValidationResult))
        {
        }

        public ValidationServiceErrorException(ModelValidationResult modelValidationResult, string message)
            : base(new GeneralServiceError(), message)
        {
        }

        public ValidationServiceErrorException(ModelValidationResult modelValidationResult, string message, Exception innerException)
            : base(new GeneralServiceError(), message, innerException)
        {
        }

    }
}
