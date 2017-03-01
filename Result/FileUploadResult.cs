namespace Liquidacoes.Result
{
    using System;
    
    public class FileUploadResult
    {
        public string FileName { get; }
        public string NaoProcessadas { get; }
        public string VirtualPath { get; }

        public FileUploadResult(DateTime data, string unProcessed, string virtualPath)
        {
            FileName = $"Liquidacao{data:dd.MM.yyyy}.txt";
            NaoProcessadas = unProcessed;
            VirtualPath = virtualPath;
        }
    }
}