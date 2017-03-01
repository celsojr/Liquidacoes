namespace Liquidacoes.Handlers.Accounts.Credit
{
    using Liquidacoes.Domain;
    using System.Collections.Generic;

    internal interface ICreditHandler
    {
        IEnumerable<Account> Process(Receipt receipt);
    }
}