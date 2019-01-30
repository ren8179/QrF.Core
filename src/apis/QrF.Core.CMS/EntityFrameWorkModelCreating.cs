using Microsoft.EntityFrameworkCore;
using QrF.Core.CMS.Entities;
using QrF.Core.ComFr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS
{
    class EntityFrameWorkModelCreating : IOnModelCreating
    {
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<NavigationEntity>();
        }
    }
}
