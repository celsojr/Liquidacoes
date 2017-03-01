namespace Liquidacoes.Infrastructure.Specification.Exceptions
{
    using System;
    using Liquidacoes.Infrastructure.Errors.Exceptions;
    using Liquidacoes.Infrastructure.Errors.Specification.Errors;

    public class GeneralServiceErrorException : HttpServiceErrorException
    {
        public GeneralServiceErrorException()
            : base(new GeneralServiceError()) 
        {
        }

        public GeneralServiceErrorException(string message)
            : base(new GeneralServiceError(), message) 
        {
        }

        public GeneralServiceErrorException(string message, Exception innerException)
            : base(new GeneralServiceError(), message, innerException) 
        {
        }

    }
}
