namespace Liquidacoes.Infrastructure.Settings
{
    using Liquidacoes.Domain;

    public class ApplicationSettings : IApplicationSettings
    {
        public AccountSettings AccountSettings { get; set; }

        public PaymentApi PaymentApi { get; set; }

        public FileSettings FileSettings { get; set; }
        
        public string GetContaCredito(string cnpj)
            => AccountSettings.ContaCredito + cnpj.Replace(cnpj.Substring(4, 8), "");

        public string GetHistory(bool isDebitAccount, Receipt receipt = null)
            => isDebitAccount 
                ? "REC. CLIENTES DIVERSOS ITAÚ."
                : $"REC.NF. {receipt.Nf} – {receipt.RazaoSocial.TrimEnd('.')}.";
    }

    public class AccountSettings
    {
        public string ContaDebito { get; set; }
        public string ContaCredito { get; set; }
        public string ContaCreditoJuros { get; set; }
    }

    public class PaymentApi
    {
        public string BaseAddress { get; set; }
    }

    public class FileSettings
    {
        public int LineIndex { get; set; }
        public int MaxFileSize { get; set; }
    }
}