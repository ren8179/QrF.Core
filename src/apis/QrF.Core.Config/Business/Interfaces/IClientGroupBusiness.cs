using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QrF.Core.Config.Interfaces
{
    public interface IClientGroupBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<BasePageOutput<ClientGroup>> GetPageList(PageInput input);
        /// <summary>
        /// 列表
        /// </summary>
        Task<List<ClientGroup>> GetList(int? input);
        /// <summary>
        /// 分配客户端
        /// </summary>
        Task<bool> ToAccReRouteAsync(ToAccGroupInput input);
        /// <summary>
        /// 编辑信息
        /// </summary>
        Task EditModel(ClientGroup input);
        /// <summary>
        /// 删除
        /// </summary>
        Task DelModel(int input);
    }
}
