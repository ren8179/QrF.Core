using QrF.Core.CMS.Entities;
using QrF.Core.ComFr;
using QrF.Core.ComFr.Repositories;
using QrF.Core.Utils.Extension;
using System;
using System.Linq;

namespace QrF.Core.CMS.Service
{
    public class ArticleService : ServiceBase<ArticleEntity, CMSDbContext>, IArticleService
    {
        public ArticleService(IApplicationContext applicationContext, CMSDbContext dbContext)
            : base(applicationContext, dbContext)
        {

        }

        public override ServiceResult<ArticleEntity> Add(ArticleEntity item)
        {
            if (!item.Url.IsNullOrWhiteSpace())
            {
                if (GetByUrl(item.Url) != null)
                {
                    var result = new ServiceResult<ArticleEntity>();
                    result.RuleViolations.Add(new RuleViolation("Url", "Url已存在"));
                    return result;
                }
            }
            return base.Add(item);
        }

        public ArticleEntity GetByUrl(string url)
        {
            return Get(m => m.Url == url).FirstOrDefault();
        }

        public ArticleEntity GetNext(ArticleEntity article)
        {
            return CurrentDbSet.Where(m => m.IsPublish && m.ArticleTypeID == article.ArticleTypeID && m.PublishDate > article.PublishDate && m.ID != article.ID).OrderBy(m => m.PublishDate).ThenBy(m => m.ID).FirstOrDefault();
        }

        public ArticleEntity GetPrev(ArticleEntity article)
        {
            return CurrentDbSet.Where(m => m.IsPublish && m.ArticleTypeID == article.ArticleTypeID && m.PublishDate < article.PublishDate && m.ID != article.ID).OrderByDescending(m => m.PublishDate).ThenByDescending(m => m.ID).FirstOrDefault();
        }

        public void IncreaseCount(ArticleEntity article)
        {
            article.Counter = (article.Counter ?? 0) + 1;
            DbContext.Attach(article);
            DbContext.Entry(article).Property(x => x.Counter).IsModified = true;
            DbContext.SaveChanges();
        }

        public void Publish(int ID)
        {
            var article = Get(ID);
            article.IsPublish = true;
            article.PublishDate = DateTime.Now;
            Update(article);
        }
    }
}
