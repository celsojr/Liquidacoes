namespace Liquidacoes.Modules.Liquidacao
{
    using Nancy;
    using log4net;
    using Liquidacoes.Response;
    using Liquidacoes.Modules.Common;
    using Liquidacoes.Requests.Model;
    using Liquidacoes.Modules.EventFeed;
    using Liquidacoes.Handlers.FileUpload;

    public class LiquidacaoModule : NancyModule
    {
        private static ILog log = LogManager.GetLogger(typeof(LiquidacaoModule));

        public LiquidacaoModule(
            IFileUploadHandler fileUploadHandler,
            ILiquidacaoStore liquidacaoStore,
            IEventStore eventStore) 
            : base("/file")
        {
            Get("/download/{virtualPath}/{filename}", async (args, _) =>
            {
                var userId = int.TryParse(this.Request.Query.userId.Value, out int id) ? id : 0;
                var liquidacao = liquidacaoStore.Get(userId);

                var item = (LiquidacaoItem) liquidacao.GetItem(args.virtualPath);
                if (item == null)
                {
                    return HttpStatusCode.NotFound;
                }

                var fileName = (string) args.filename;
                var mimeType = MimeTypes.GetMimeType(fileName);

                return Response
                    .FromStream(await this.GetStreamAsync(item.Content)
                    .ConfigureAwait(false), mimeType)
                    .AsAttachment(fileName);
            });

            Post("/upload", async (args, _) =>
            {
                var request = this.CustomBindAndValidate<FileUploadRequest>();

                var fileUploadResult = await fileUploadHandler
                    .HandleUploadAsync(request.UserId, request.File, liquidacaoStore, eventStore)
                    .ConfigureAwait(false);

                var response = new FileUploadResponse(fileUploadResult);

                log.Debug($"http://localhost:5001/{response.DownloadLink}?userId={request.UserId}");

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(response);
            });
        }
    }
}
