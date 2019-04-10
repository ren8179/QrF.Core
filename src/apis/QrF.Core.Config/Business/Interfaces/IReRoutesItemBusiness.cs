using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QrF.Core.Config.Interfaces
{
    public interface IReRoutesItemBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<BasePageOutput<ReRoutesItem>> GetPageList(PageInput input);
        /// <summary>
        /// 查询所有分类
        /// </summary>
        Task<List<ReRoutesItem>> GetAllList();
        /// <summary>
        /// 编辑信息
        /// </summary>
        Task EditModel(ReRoutesItem input);
        /// <summary>
        /// 删除
        /// </summary>
        Task DelModel(int input);
    }
}
