using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Repositories
{
    public interface IOnModelCreating
    {
        void OnModelCreating(ModelBuilder modelBuilder);
    }
}
