namespace Liquidacoes.Handlers.Accounts.Credit
{
    using System;
    using Liquidacoes.Domain;
    using System.Collections.Generic;
    using Liquidacoes.Infrastructure.Settings;
    using Liquidacoes.Infrastructure.Repository;
    using static System.Globalization.CultureInfo;

    internal sealed class CreditHandler : AccountHandler<Account, Guid>, ICreditHandler
    {
        private readonly IApplicationSettings appSettings;

        public CreditHandler()
        {
        }

        public CreditHandler(IApplicationSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        protected override Account Initialize(Account account, params object[] @params)
        {
            return new Account
            {
                Data = string.Empty,
                ContaDebito = string.Empty,
                ContaCredito = string.Empty,
                CentroCusto = string.Empty,
                Historico = string.Empty,
                Especie = string.Empty,
                Fcont = string.Empty
            };
        }

        public IEnumerable<Account> Process(Receipt receipt)
        {
            var data = receipt.Recebimento.ToString("ddMM");

            var creditAccount = this.Initialize(new Account());
            creditAccount.Data = data;
            creditAccount.Historico = appSettings?.GetHistory(false, receipt);
            creditAccount.ContaCredito = appSettings?.GetContaCredito(receipt.Cnpj);
            creditAccount.Especie = receipt.Liquido.ToString("0.00", InvariantCulture);
            yield return creditAccount;

            var interest = receipt.Juros > 0;
            if (interest)
            {
                var interestAccount = this.Initialize(new Account());
                interestAccount.Data = data;
                interestAccount.Historico = appSettings?.GetHistory(false, receipt);
                interestAccount.Especie = receipt.Juros.ToString("0.00", InvariantCulture);
                interestAccount.ContaCredito = appSettings?.AccountSettings?.ContaCreditoJuros;
                yield return interestAccount;
            }
        }
    }
}