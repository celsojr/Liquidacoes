namespace Liquidacoes.Handlers.Receipts
{
    using System;
    using Liquidacoes.Domain;
    using Liquidacoes.Result;
    using System.Threading.Tasks;
    using Liquidacoes.Handlers.Model;

    internal interface IReceiptHandler : IDisposable
    {
        void SetReceiptDate(DateTime date);
        Task ProccessReceiptAsync(Receipt receipt);
        void ProcessDebit(HandlerUploadModel model);

        FileUploadResult FileUploadResult(HandlerUploadModel model);
        FileUploadResult FileUploadResult(HandlerUploadModel model, bool unprocessed);
        FileUploadResult FileUploadResult(HandlerUploadModel model, string unprocessed);
    }
}