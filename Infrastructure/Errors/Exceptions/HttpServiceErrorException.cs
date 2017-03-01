namespace Liquidacoes.Infrastructure.Errors.Exceptions
{
    using System;
    using Liquidacoes.Infrastructure.Errors.Model;

    public abstract class HttpServiceErrorException : Exception
    {
        public readonly HttpServiceError HttpServiceError;

        public HttpServiceErrorException(HttpServiceError serviceError)
            : base() 
        {
            this.HttpServiceError = serviceError;
        }

        public HttpServiceErrorException(HttpServiceError serviceError, string message)
            : base(message) 
        {
            this.HttpServiceError = serviceError;
        }

        public HttpServiceErrorException(HttpServiceError serviceError, string message, Exception innerException)
            : base(message, innerException) 
        {
            this.HttpServiceError = serviceError;
        }

    }
}