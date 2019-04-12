using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QrF.Core.Config.Interfaces
{
    public interface IReRouteGroupAuthBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<BasePageOutput<ReRouteGroupAuth>> GetPageList(PageInput input);
        /// <summary>
        /// 列表
        /// </summary>
        Task<List<ReRouteGroupAuth>> GetList(int? input);
        /// <summary>
        /// 分配路由
        /// </summary>
        Task<bool> ToAccReRouteAsync(ToAccReRouteInput input);
        /// <summary>
        /// 编辑信息
        /// </summary>
        Task EditModel(ReRouteGroupAuth input);
        /// <summary>
        /// 删除
        /// </summary>
        Task DelModel(int input);
    }
}
