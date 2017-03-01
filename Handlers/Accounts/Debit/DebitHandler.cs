namespace Liquidacoes.Handlers.Accounts.Debit
{
    using System;
    using Liquidacoes.Domain;
    using Liquidacoes.Infrastructure.Settings;
    using Liquidacoes.Infrastructure.Repository;
    using static System.Globalization.CultureInfo;

    internal sealed class DebitHandler : AccountHandler<Account, Guid>, IDebitHandler
    {
        private readonly decimal total;
        private readonly DateTime receiptDate;
        private readonly IApplicationSettings appSettings;
        
        public DebitHandler()
        {
        }

        public DebitHandler(IApplicationSettings appSettings, DateTime receiptDate, decimal total)
            : this(appSettings)
        {
            this.total = total;
            this.receiptDate = receiptDate;
        }

        public DebitHandler(IApplicationSettings appSettings)
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

        public Account Process()
        {
            var debitAccount = this.Initialize(new Account());
            debitAccount.Data = receiptDate.ToString("ddMM");
            debitAccount.ContaDebito = appSettings?.AccountSettings?.ContaDebito;
            debitAccount.Historico = appSettings?.GetHistory(true);
            debitAccount.Especie = total.ToString("0.00", InvariantCulture);
            return debitAccount;
        }
    }
}