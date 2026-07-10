using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestWorker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Logging.ClearProviders();
            var app = builder.Build();

            app.Run();
        }
    }
}
