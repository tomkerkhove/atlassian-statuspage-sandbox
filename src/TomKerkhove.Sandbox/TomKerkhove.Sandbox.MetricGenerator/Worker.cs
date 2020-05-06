using System;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TomKerkhove.Sandbox.MetricGenerator.Statuspage;

namespace TomKerkhove.Sandbox.MetricGenerator
{
    public class Worker : BackgroundService
    {
        private readonly AtlassianStatuspage _atlassianStatuspage;
        private readonly Faker _bogusGenerator = new Faker();
        private readonly ILogger<Worker> _logger;

        public Worker(AtlassianStatuspage atlassianStatuspage, ILogger<Worker> logger)
        {
            _atlassianStatuspage = atlassianStatuspage;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ReportRequestDropRateAsync();
                await ReportWebhookFailuresAsync();

                await Task.Delay(6000, stoppingToken);
            }
        }

        private async Task ReportRequestDropRateAsync()
        {
            var requestDropRate = _bogusGenerator.Random.Int(0, 20);
            _logger.LogInformation("Reporting request drop rate of {failureRate}%", requestDropRate);
            await _atlassianStatuspage.ReportMetricAsync("d0lflb8w18lx", requestDropRate, "<api-key>");
        }

        private async Task ReportWebhookFailuresAsync()
        {
            var webhookFailures = _bogusGenerator.Random.Int(0, 1337);
            _logger.LogInformation("Reporting webhook failures {failureRate}%", webhookFailures);
            await _atlassianStatuspage.ReportMetricAsync("lx29ly1skzpq", webhookFailures, "<api-key>");
        }
    }
}
