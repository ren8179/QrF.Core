using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.EntityFrameworkCore.Configuration
{
    public class DbContextConfigurerAction<TDbContext> : IDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        public Action<DbContextConfiguration<TDbContext>> Action { get; set; }

        public DbContextConfigurerAction(Action<DbContextConfiguration<TDbContext>> action)
        {
            Action = action;
        }

        public void Configure(DbContextConfiguration<TDbContext> configuration)
        {
            Action(configuration);
        }
    }
}
