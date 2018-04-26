using Castle.MicroKernel.Registration;
using Microsoft.EntityFrameworkCore;
using QrF.ABP.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.EntityFrameworkCore.Configuration
{
    public class EfCoreConfiguration : IEfCoreConfiguration
    {
        private readonly IIocManager _iocManager;

        public EfCoreConfiguration(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public void AddDbContext<TDbContext>(Action<DbContextConfiguration<TDbContext>> action)
            where TDbContext : DbContext
        {
            _iocManager.IocContainer.Register(
                Component.For<IDbContextConfigurer<TDbContext>>().Instance(
                    new DbContextConfigurerAction<TDbContext>(action)
                ).IsDefault()
            );
        }
    }
}
