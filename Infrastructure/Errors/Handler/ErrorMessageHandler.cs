namespace Liquidacoes.Infrastructure.Errors.Handler
{
    using System;
    using Liquidacoes.Infrastructure.Errors.Enums;
    using static Liquidacoes.Infrastructure.Errors.Services.ErrorServices;

    internal class ErrorMessageHandler
    {
        internal static string MessageHandler(string nf, ErrorEnum error)
        {
            var tuple = new Tuple<string, ErrorEnum>(nf, error);
            return GetErrorMessage(tuple);
        }
    }
}