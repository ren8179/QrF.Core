using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.EntityFrameworkCore.Configuration
{
    public interface IDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        void Configure(DbContextConfiguration<TDbContext> configuration);
    }
}
