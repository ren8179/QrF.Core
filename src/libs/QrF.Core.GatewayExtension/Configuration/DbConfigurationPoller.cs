﻿using Microsoft.Extensions.Hosting;
using Ocelot.Configuration.Creator;
using Ocelot.Configuration.Repository;
using Ocelot.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QrF.Core.GatewayExtension.Configuration
{
    public class DbConfigurationPoller : IHostedService, IDisposable
    {
        private readonly IOcelotLogger _logger;
        private readonly IFileConfigurationRepository _repo;
        private readonly CusOcelotConfiguration _option;
        private Timer _timer;
        private bool _polling;
        private readonly IInternalConfigurationRepository _internalConfigRepo;
        private readonly IInternalConfigurationCreator _internalConfigCreator;
        public DbConfigurationPoller(IOcelotLoggerFactory factory,
            IFileConfigurationRepository repo,
            IInternalConfigurationRepository internalConfigRepo,
            IInternalConfigurationCreator internalConfigCreator,
            CusOcelotConfiguration option)
        {
            _internalConfigRepo = internalConfigRepo;
            _internalConfigCreator = internalConfigCreator;
            _logger = factory.CreateLogger<DbConfigurationPoller>();
            _repo = repo;
            _option = option;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_option.EnableTimer)
            {//判断是否启用自动更新
                _logger.LogInformation($"{nameof(DbConfigurationPoller)} is starting.");
                _timer = new Timer(async x =>
                {
                    if (_polling)
                    {
                        return;
                    }
                    _polling = true;
                    await Poll();
                    _polling = false;
                }, null, _option.TimerDelay, _option.TimerDelay);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_option.EnableTimer)
            {//判断是否启用自动更新
                _logger.LogInformation($"{nameof(DbConfigurationPoller)} is stopping.");
                _timer?.Change(Timeout.Infinite, 0);
            }
            return Task.CompletedTask;
        }

        private async Task Poll()
        {
            var fileConfig = await _repo.Get();
            if (fileConfig.IsError)
            {
                _logger.LogWarning($"error geting file config, errors are {string.Join(",", fileConfig.Errors.Select(x => x.Message))}");
                return;
            }
            else
            {
                var config = await _internalConfigCreator.Create(fileConfig.Data);
                if (!config.IsError)
                {
                    _internalConfigRepo.AddOrReplace(config.Data);
                }
            }
        }
    }
}
