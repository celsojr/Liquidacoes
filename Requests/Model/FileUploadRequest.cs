namespace Liquidacoes.Requests.Model
{
    using Nancy;
    
    public class FileUploadRequest
    {
        public int UserId { get; set; }
        public HttpFile File { get; set; }
    }
}