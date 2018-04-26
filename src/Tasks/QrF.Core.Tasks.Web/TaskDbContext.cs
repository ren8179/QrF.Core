using Microsoft.EntityFrameworkCore;
using QrF.Core.EntityFrameworkCore;
using QrF.Core.Tasks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Tasks.Web
{
    public class TaskDbContext : BaseDbContext
    {
        public DbSet<ProjectTask> Products { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
        }
    }
}
