namespace Liquidacoes.Infrastructure.Services
{
    using System;
    // using log4net;
    using System.Net.Http;
    // using System.Diagnostics;
    using System.Threading.Tasks;
    using Liquidacoes.Infrastructure.Settings;

    internal class PaymentService
    {
        // private static ILog log = LogManager.GetLogger(typeof(PaymentSevice));

        private readonly IApplicationSettings appSettings;

        public PaymentService()
        {
        }

        public PaymentService(IApplicationSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public async Task<bool> GetPaymentAsync(string nf, int boleto)
        {
            using(var client = new HttpClient())
            {
                var baseAddress = appSettings?.PaymentApi?.BaseAddress;
                var uri = $"{baseAddress}/payment/{nf}/{boleto}";

                var response = await client.GetAsync(new Uri(uri)).ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
        }
    }
}