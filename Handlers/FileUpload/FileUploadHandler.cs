namespace Liquidacoes.Handlers.FileUpload
{
    using Nancy;
    using System;
    using System.IO;
    using Liquidacoes.Result;
    using System.Threading.Tasks;
    using Infrastructure.Settings;
    using Liquidacoes.Handlers.Model;
    using Liquidacoes.Handlers.Receipts;
    using Liquidacoes.Modules.EventFeed;
    using Liquidacoes.Modules.Liquidacao;
    using Liquidacoes.Infrastructure.Errors.Enums;
    using Liquidacoes.Infrastructure.Validation.Line;
    using static Liquidacoes.Infrastructure.Errors.Handler.ErrorMessageHandler;

    internal sealed class FileUploadHandler : ReceiptHandler, IFileUploadHandler
    {
        public FileUploadHandler()
        {
        }

        public FileUploadHandler(IApplicationSettings appSettings) 
            : base(appSettings)
        {
        }

        public async Task<FileUploadResult> HandleUploadAsync(
            int userId,
            HttpFile file,
            ILiquidacaoStore liquidacaoStore,
            IEventStore eventStore)
        {
            using(var reader = new StreamReader(file.Value))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var currentLine = LineValidation
                                    .ValidateLine(line)
                                    .IsHeader()
                                    .IsEmpty()
                                    .IsOk();

                    if (currentLine.IsValid)
                    {
                        var receipt = currentLine.Receipt;
                        SetReceiptDate(receipt.Recebimento);

                        if (LineValidation.ValidateCnpj(receipt.Cnpj))
                        {
                            var errorMessage = MessageHandler(receipt.Nf, ErrorEnum.CnpjInvalido);
                            Unprocessed.AppendLine(errorMessage);
                            continue;
                        }

                        await ProccessReceiptAsync(receipt);
                    }
                }
            }

            var hum = new HandlerUploadModel
            {
                UserId = userId,
                LiquidacaoStore = liquidacaoStore,
                EventStore = eventStore,
                VirtualPath = Guid.NewGuid().ToString().Replace("-", "")
            };

            return FileUploadResult(hum, true);
        }
        
    }
}