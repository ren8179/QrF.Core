using QrF.Core.Admin.Dto;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Interfaces
{
    public interface ILogBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BasePageQueryOutput<LogDto>> GetPageList(QueryLogsInput input);
    }
}
