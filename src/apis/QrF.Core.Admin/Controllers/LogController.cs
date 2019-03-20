using Microsoft.AspNetCore.Mvc;
using QrF.Core.Admin.Domain;
using QrF.Core.Admin.Dto;
using QrF.Core.Admin.Interfaces;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Controllers
{
    /// <summary>
    /// 日志管理
    /// </summary>
    [Route("AdminAPI/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogBusiness _business;
        public LogController(ILogBusiness business)
        {
            _business = business;
        }
        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetPageList")]
        public async Task<BasePageQueryOutput<LogDto>> QueryLogsAsync([FromQuery] QueryLogsInput input)
        {
            return await _business.GetPageList(input);
        }

        /// <summary>
        /// 获取对象信息
        /// </summary>
        [HttpGet("GetModel")]
        public async Task<Log> GetModelAsync(int id)
        {
            var model = await _business.GetModelAsync(id);
            return model;
        }

    }
}
