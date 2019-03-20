using QrF.Core.Admin.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Interfaces
{
    public interface IOrganizeBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BasePageQueryOutput<QueryOrganizeDto>> GetPageList(QueryOrganizesInput input);
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<IEnumerable<QueryOrganizeDto>> GetListAsync(Guid? deptId);
        /// <summary>
        /// 
        /// </summary>
        Task<OrgDto> GetModelAsync(Guid id);
        /// <summary>
        /// 编辑信息
        /// </summary>
        Task EditModel(OrgDto input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DelModel(Guid input);
    }
}
