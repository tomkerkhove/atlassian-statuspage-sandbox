using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TomKerkhove.Sandbox.MetricGenerator.Statuspage;

namespace TomKerkhove.Sandbox.MetricGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddTransient<AtlassianStatuspage>();
                    services.AddHostedService<Worker>();
                });
    }
}
