using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
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
        /// 编辑信息
        /// </summary>
        Task EditModel(ApiResource input);
        /// <summary>
        /// 删除
        /// </summary>
        Task DelModel(int input);
    }
}
