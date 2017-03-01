namespace Liquidacoes.Response
{
    using Liquidacoes.Result;

    internal class FileUploadResponse
    {
        public string DownloadLink { get; }
        public string NaoProcessadas { get; }

        public FileUploadResponse(FileUploadResult result)
        {
            DownloadLink = $"file/download/{result.VirtualPath}/{result.FileName}";
            NaoProcessadas = result.NaoProcessadas;
        }
    }
}