
using Accounts.Core;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Accounts.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOpenTelemetry()
                .WithTracing(config =>
                {
                    config.ConfigureResource(r => r.AddService("Accounts.WebApi"));
                    config.SetErrorStatusOnException();
                    config.SetSampler(new AlwaysOnSampler());
                    config.AddConsoleExporter();
                    config.AddSource("Accounts.Core");
                });

            builder.Services.AddCoreDependencies();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.MapOpenApi();
            app.UseSwaggerUi(options =>
            {
                options.DocumentPath = "openapi/v1.json";
            });

            app.MapControllers();

            app.Run();
        }
    }
}
