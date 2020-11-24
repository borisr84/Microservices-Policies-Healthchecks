using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocalPicturesService.CustomHealthChecks
{
    public class PicturesExistenceCheck : IHealthCheck
    {
        private readonly string _imagesPath;
        public PicturesExistenceCheck(string imagesPath)
        {
            _imagesPath = imagesPath;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(_imagesPath) || Directory.GetFiles(_imagesPath).Length == 0)
                return new HealthCheckResult(HealthStatus.Degraded);

            return new HealthCheckResult(HealthStatus.Healthy);
        }
    }
}
