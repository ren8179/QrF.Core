using Microsoft.EntityFrameworkCore;
using QrF.Core.ComFr.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr
{
    public class ComFrDbContext : DbContextBase
    {
        public ComFrDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }


    }
}
