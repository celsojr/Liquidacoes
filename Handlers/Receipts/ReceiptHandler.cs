namespace Liquidacoes.Handlers.Receipts
{
    using System;
    using System.Linq;
    using System.Text;
    using Liquidacoes.Domain;
    using Liquidacoes.Result;
    using System.Threading.Tasks;
    using Liquidacoes.Handlers.Model;
    using Liquidacoes.Handlers.Common;
    using Liquidacoes.Modules.Liquidacao;
    using Liquidacoes.Handlers.Accounts.Debit;
    using Liquidacoes.Infrastructure.Services;
    using Liquidacoes.Infrastructure.Settings;
    using Liquidacoes.Handlers.Accounts.Credit;
    using Liquidacoes.Infrastructure.Errors.Enums;
    using static Liquidacoes.Infrastructure.Errors.Handler.ErrorMessageHandler;

    internal abstract class ReceiptHandler : Handler<ReceiptHandler>, IReceiptHandler
    {
        private static int? lineIndex;
        private static DateTime receiptDate;

        private readonly IApplicationSettings appSettings;

        private decimal debitAccountTotal { get; set; } = decimal.Zero;

        public StringBuilder Content { get; private set; } = new StringBuilder();
        public StringBuilder Unprocessed { get; private set; } = new StringBuilder();

        protected ReceiptHandler()
        {
        }

        protected ReceiptHandler(IApplicationSettings appSettings)
        {
            this.appSettings = appSettings;

             lineIndex = appSettings?.FileSettings?.LineIndex ?? 0;
        }

        public void SetReceiptDate(DateTime date)
        {
            if (receiptDate == DateTime.MinValue)
            {
                receiptDate = date;
            }
        }

        public async Task ProccessReceiptAsync(Receipt receipt)
        {
            ++lineIndex;

            if (!int.TryParse(receipt.Boleto, out int boleto))
            {
                var currentLine = lineIndex;
                lineIndex = appSettings?.FileSettings?.LineIndex ?? 0;
                throw new Exception($"Campo 'Boleto (nº)' vazio na linha: {currentLine}");
            }

            var paymentService = new PaymentService(appSettings);
            bool hasPayment = await paymentService.GetPaymentAsync(receipt.Nf, boleto);
            
            if (!hasPayment)
            {
                var message = MessageHandler(receipt.Nf, ErrorEnum.PgtoNaoEncontrado);
                Unprocessed.AppendLine(message);
                return;
            }

            var credit = new CreditHandler(appSettings);
            credit.Process(receipt).ToList()
                .ForEach(account =>
                    Content.Append(account).AppendLine());

            debitAccountTotal += (receipt.Liquido + receipt.Juros);
        }

        public void ProcessDebit(HandlerUploadModel model)
        {
            if (model.VirtualPath.IsNull())
            {
                throw new InvalidOperationException($"{nameof(model.VirtualPath)} should not be null");
            }

            var debit = new DebitHandler(appSettings, receiptDate, debitAccountTotal);
            Content.Prepend(debit.Process()).AppendLine();

            var item = new LiquidacaoItem
            {
                VirtualPath = model.VirtualPath,
                Content = Content.ToString()
            };

            var liquidacao = model.LiquidacaoStore.Get(model.UserId);
            liquidacao.AddItems(item, model.EventStore);
            model.LiquidacaoStore.Save(liquidacao);
        }
        
        /// <summary>
        /// Generates a file upload result
        /// </summary>
        /// <param name="unprocessed">The unprocessed data</param>
        /// <param name="virtualPath">The virtual download path</param>
        /// <returns>A file upload result</returns>
        public FileUploadResult FileUploadResult(HandlerUploadModel model, string unprocessed)
        {
            ProcessDebit(model);

            var fur = new FileUploadResult(receiptDate, unprocessed, model.VirtualPath);

            this.Dispose(false);
            
            return fur;
        }

        /// <summary>
        /// Generates a file upload result
        /// </summary>
        /// <param name="virtualPath">The virtual download path</param>
        /// <returns>A file upload result with no unprocessed data</returns>
        public FileUploadResult FileUploadResult(HandlerUploadModel model)
        {
            ProcessDebit(model);

            var fur = new FileUploadResult(receiptDate, null, model.VirtualPath);

            this.Dispose(false);
            
            return fur;
        }

        /// <summary>
        /// Generates a file upload result
        /// </summary>
        /// <param name="unprocessed"><see langword="true"/> if it should contains unprocessed data, otherwise <see langword="false"/></param>
        /// <param name="virtualPath">The virtual download path</param>
        /// <returns>If true, returns a file upload result with the unprocessed data, otherwise the unprocessed data will be null</returns>
        public FileUploadResult FileUploadResult(HandlerUploadModel model, bool unprocessed)
        {
            ProcessDebit(model);

            var uContent = unprocessed ? Unprocessed.ToString() : null;
            var fur = new FileUploadResult(receiptDate, uContent, model.VirtualPath);

            this.Dispose(false);
            
            return fur;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) 
            {
                // Clean up mana­ged objects that are dis­po­sa­ble
            }

            // Clean up unmanaged objects or, in our case,
            // mana­ged objects that are not disposable
            Content.Clear();
            Unprocessed.Clear();
            receiptDate = DateTime.MinValue;
            debitAccountTotal = decimal.Zero;
        }

        // ~ReceiptHandler()
        // {
        //     Dispose(false);
        // }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}