namespace Liquidacoes
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;

    internal class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5001/")
                .Build();

            host.Run();
        }
    }
}