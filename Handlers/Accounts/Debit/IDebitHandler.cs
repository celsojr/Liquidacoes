namespace Liquidacoes.Handlers.Accounts.Debit
{
    using Liquidacoes.Domain;

    internal interface IDebitHandler
    {
        Account Process();
    }
}