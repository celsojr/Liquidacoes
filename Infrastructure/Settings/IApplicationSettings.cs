namespace Liquidacoes.Infrastructure.Settings
{
    using Liquidacoes.Domain;
    
    public interface IApplicationSettings
    {
        AccountSettings AccountSettings { get; }

        PaymentApi PaymentApi { get; }

        FileSettings FileSettings { get; }

        string GetContaCredito(string cnpj);

        string GetHistory(bool isDebitAccount, Receipt receipt = null);
    }
}