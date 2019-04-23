using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QrF.Core.Config.Interfaces
{
    public interface IApiResourceBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<BasePageOutput<ApiResource>> GetPageList(PageInput input);
        /// <summary>
        /// 获取授权域列表
        /// </summary>
        /// <returns></returns>
        Task<List<ApiScope>> GetScopeList();
        /// <summary>
        /// 编辑信息
        /// </summary>
        Task EditModel(ApiResource input);
        /// <summary>
        /// 删除
        /// </summary>
        Task DelModel(int input);
    }
}
