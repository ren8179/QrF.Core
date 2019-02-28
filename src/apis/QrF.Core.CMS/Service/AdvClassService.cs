using QrF.Core.CMS.Entities;
using QrF.Core.ComFr;
using QrF.Core.ComFr.Repositories;
using QrF.Core.Utils.Extension;
using System;
using System.Linq;

namespace QrF.Core.CMS.Service
{
    public class AdvClassService : ServiceBase<AdvClassEntity, CMSDbContext>, IAdvClassService
    {
        public AdvClassService(IApplicationContext applicationContext, CMSDbContext dbContext)
            : base(applicationContext, dbContext)
        {

        }
        
    }
}
