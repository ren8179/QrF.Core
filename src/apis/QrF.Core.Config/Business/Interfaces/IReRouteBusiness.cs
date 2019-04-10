using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using System.Threading.Tasks;

namespace QrF.Core.Config.Interfaces
{
    public interface IReRouteBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<BasePageOutput<ReRoute>> GetPageList(PageInput input);
        /// <summary>
        /// 编辑信息
        /// </summary>
        Task EditModel(ReRoute input);
        /// <summary>
        /// 删除
        /// </summary>
        Task DelModel(int input);
    }
}
