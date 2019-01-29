using Microsoft.EntityFrameworkCore;
using QrF.Core.CMS.Entities;
using QrF.Core.ComFr;
using QrF.Core.ComFr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS
{
    public class CMSDbContext : ComFrDbContext
    {
        public CMSDbContext(DbContextOptions<CMSDbContext> dbContextOptions, IEnumerable<IOnModelCreating> modelCreatings) : base(dbContextOptions)
        {
            ModelCreatings = modelCreatings;
        }

        public DbSet<NavigationEntity> Navigation { get; set; }

    }
}
