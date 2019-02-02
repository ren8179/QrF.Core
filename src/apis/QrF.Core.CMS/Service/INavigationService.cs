using QrF.Core.ComFr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.CMS.Service
{
    public interface INavigationService : IService<Entities.NavigationEntity>
    {
        void Move(string id, string parentId, int position);
    }
}
