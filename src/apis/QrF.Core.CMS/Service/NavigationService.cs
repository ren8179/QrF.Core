using Microsoft.EntityFrameworkCore;
using QrF.Core.CMS.Entities;
using QrF.Core.ComFr;
using QrF.Core.ComFr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QrF.Core.Utils.Extension;
using System.Linq.Expressions;

namespace QrF.Core.CMS.Service
{
    public class NavigationService : ServiceBase<NavigationEntity, CMSDbContext>, INavigationService
    {
        public NavigationService(IApplicationContext applicationContext, CMSDbContext dbContext) : base(applicationContext, dbContext)
        {
        }
        public override DbSet<NavigationEntity> CurrentDbSet => DbContext.Navigation;
        public override ServiceResult<NavigationEntity> Add(NavigationEntity item)
        {
            if (item.ParentId.IsNullOrEmpty())
            {
                item.ParentId = "#";
            }
            item.ID = Guid.NewGuid().ToString("N");
            return base.Add(item);
        }
        public override void Remove(NavigationEntity item)
        {
            Remove(m => m.ParentId == item.ID);
            base.Remove(item);
        }

        public override void RemoveRange(params NavigationEntity[] items)
        {
            items.Each(m =>
            {
                Remove(n => n.ParentId == m.ID);
            });
            base.RemoveRange(items);
        }
        public override void Remove(Expression<Func<NavigationEntity, bool>> filter)
        {
            Get(filter).Each(m =>
            {
                Remove(n => n.ParentId == m.ID);
            });
            base.Remove(filter);
        }

        public void Move(string id, string parentId, int position, int oldPosition)
        {
            var nav = Get(id);
            nav.ParentId = parentId;
            nav.DisplayOrder = position;

            IEnumerable<NavigationEntity> navs = CurrentDbSet.AsTracking().Where(m => m.ParentId == nav.ParentId && m.ID != nav.ID).OrderBy(m => m.DisplayOrder);

            int order = 1;
            for (int i = 0; i < navs.Count(); i++)
            {
                var eleNav = navs.ElementAt(i);
                if (i == position - 1)
                {
                    order++;
                }
                eleNav.DisplayOrder = order;
                order++;
            }
            Update(nav);
        }
    }
}
