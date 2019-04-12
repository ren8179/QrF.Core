using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QrF.Core.Config.Interfaces
{
    public interface IAuthGroupBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<BasePageOutput<AuthGroup>> GetPageList(PageInput input);
        /// <summary>
        /// 编辑信息
        /// </summary>
        Task EditModel(AuthGroup input);
        /// <summary>
        /// 删除
        /// </summary>
        Task DelModel(int input);
    }
}
