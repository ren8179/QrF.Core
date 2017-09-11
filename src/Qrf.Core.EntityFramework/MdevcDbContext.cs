using Microsoft.EntityFrameworkCore;
using System;

namespace Qrf.Core.EntityFramework
{
    public class MdevcDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public MdevcDbContext(DbContextOptions<MdevcDbContext> options) : base(options)
        {
        }

    }
}
