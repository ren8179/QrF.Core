using Autofac;
using Microsoft.Extensions.Configuration;

namespace QrF.Core.Infrastructure.Modules
{
    public class MainModule : Module
    {
        private readonly IConfiguration _configuration;

        public MainModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new SettingsModule(_configuration));
            //builder.RegisterInstance(AutoMapperConfig.Initialize());
            builder.RegisterModule<StorageModule>();
            builder.RegisterModule<CqrsModule>();
            builder.RegisterModule<MediatRModule>();
        }
    }
}
