using MassTransit;
using MassTransit.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace TestConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
 
                });
            });

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(serviceName: builder.Environment.ApplicationName))
                .WithTracing(b => b
                    .AddSource(DiagnosticHeaders.DefaultListenerName)
                    .AddConsoleExporter());
                
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddOpenTelemetry();

            var app = builder.Build();

            app.Run();
        }
    }
}
