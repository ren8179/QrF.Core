using QrF.Core.CMS.Entities;
using QrF.Core.ComFr;
using QrF.Core.ComFr.Repositories;
using QrF.Core.Utils.Extension;
using System.Collections.Generic;
using System.Linq;

namespace QrF.Core.CMS.Service
{
    public class ArticleTypeService : ServiceBase<ArticleType, CMSDbContext>, IArticleTypeService
    {
        private readonly IArticleService _articleService;
        public ArticleTypeService(IApplicationContext applicationContext, IArticleService articleService, CMSDbContext dbContext)
            : base(applicationContext, dbContext)
        {
            _articleService = articleService;
        }
        public override ServiceResult<ArticleType> Add(ArticleType item)
        {
            item.ParentID = item.ParentID ?? 0;
            if (!item.Url.IsNullOrWhiteSpace())
            {
                if (GetByUrl(item.Url) != null)
                {
                    var result = new ServiceResult<ArticleType>();
                    result.RuleViolations.Add(new RuleViolation("Url", "Url已存在"));
                    return result;
                }
            }
            return base.Add(item);
        }
        public override ServiceResult<ArticleType> Update(ArticleType item)
        {
            if (!item.Url.IsNullOrWhiteSpace())
            {
                if (Count(m => m.Url == item.Url && m.ID != item.ID) > 0)
                {
                    var result = new ServiceResult<ArticleType>();
                    result.RuleViolations.Add(new RuleViolation("Url", "Url已存在"));
                    return result;
                }
            }
            return base.Update(item);
        }
        public ArticleType GetByUrl(string url)
        {
            return Get(m => m.Url == url).FirstOrDefault();
        }

        public IEnumerable<ArticleType> GetChildren(long id)
        {
            return Get(m => m.ParentID == id);
        }
        public override void Remove(ArticleType item)
        {
            if (item != null)
            {
                GetChildren(item.ID).Each(m =>
                {
                    _articleService.Remove(n => n.ArticleTypeID == m.ID);
                    Remove(m.ID);
                });
                _articleService.Remove(n => n.ArticleTypeID == item.ID);
            }
            base.Remove(item);
        }
    }
}
