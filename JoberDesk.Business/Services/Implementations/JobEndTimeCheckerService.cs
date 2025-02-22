using JoberDesk.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Implementations
{
    public class JobEndTimeCheckerService:BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<JobEndTimeCheckerService> _logger;

        public JobEndTimeCheckerService(IServiceScopeFactory serviceScopeFactory, ILogger<JobEndTimeCheckerService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service basladi.");
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _jobService = scope.ServiceProvider.GetRequiredService<IJobService>();
                    await _jobService.MarkOldJobsAsDeleted();
                }
                await Task.Delay(TimeSpan.FromHours(4), stoppingToken);
            }
        }
    }
}
