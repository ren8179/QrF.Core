using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Repositories
{
    public class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public IEnumerable<IOnModelCreating> ModelCreatings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            if (ModelCreatings != null)
            {
                foreach (var item in ModelCreatings)
                {
                    item.OnModelCreating(modelBuilder);
                }
            }
        }
    }
}
