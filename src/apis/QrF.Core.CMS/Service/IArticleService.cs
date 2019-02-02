using QrF.Core.CMS.Entities;
using QrF.Core.ComFr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS.Service
{
    public interface IArticleService : IService<ArticleEntity>
    {
        void Publish(int ID);
        void IncreaseCount(ArticleEntity article);
        ArticleEntity GetPrev(ArticleEntity article);
        ArticleEntity GetNext(ArticleEntity article);
        ArticleEntity GetByUrl(string url);
    }
}
