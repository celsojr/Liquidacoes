namespace Liquidacoes.Infrastructure.Errors.Services
{
    
    using System;
    using System.Reflection;
    using System.ComponentModel;
    using Liquidacoes.Infrastructure.Errors.Enums;

    internal class ErrorServices
    {
        internal static string GetErrorMessage(Tuple<string, ErrorEnum> tuple)
            => $"{tuple?.Item1} - Motivo: {GetEnumDescription(tuple?.Item2)}";

        internal static string GetErrorMessage(ErrorEnum value) => GetEnumDescription(value);

        private static string GetEnumDescription(Enum value)
        {
            var fi = value?.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi?.GetCustomAttributes(
                typeof(DescriptionAttribute), false);
        
            return attributes?.Length > 0 ? attributes[0]?.Description : value?.ToString();
        }
    }
}