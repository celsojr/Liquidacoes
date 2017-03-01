namespace Liquidacoes.Handlers.Common
{
    using System;
    using System.Text;
    using Liquidacoes.Domain;

    internal static class HandlerExtensions
    {
        public static StringBuilder Prepend(this StringBuilder sb, Account content)
             => sb.Insert(0, content + Environment.NewLine);

        public static bool IsNull(this string value) => string.IsNullOrWhiteSpace(value);
    }
}