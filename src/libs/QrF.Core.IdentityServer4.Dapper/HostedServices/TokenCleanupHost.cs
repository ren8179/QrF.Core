using Microsoft.Extensions.Hosting;
using QrF.Core.IdentityServer4.Dapper.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QrF.Core.IdentityServer4.Dapper.HostedServices
{
    public class TokenCleanupHost : IHostedService
    {
        private readonly TokenCleanup _tokenCleanup;
        private readonly DapperStoreOptions _options;

        public TokenCleanupHost(TokenCleanup tokenCleanup, DapperStoreOptions options)
        {
            _tokenCleanup = tokenCleanup;
            _options = options;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_options.EnableTokenCleanup)
            {
                _tokenCleanup.Start(cancellationToken);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_options.EnableTokenCleanup)
            {
                _tokenCleanup.Stop();
            }
            return Task.CompletedTask;
        }
    }
}
