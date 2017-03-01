namespace Liquidacoes.Handlers.FileUpload
{
    using Nancy;
    using Liquidacoes.Result;
    using System.Threading.Tasks;
    using Liquidacoes.Modules.EventFeed;
    using Liquidacoes.Modules.Liquidacao;

    public interface IFileUploadHandler
    {
        Task<FileUploadResult> HandleUploadAsync(
            int userId,
            HttpFile file,
            ILiquidacaoStore liquidacaoStore,
            IEventStore eventStore);
    }
}
