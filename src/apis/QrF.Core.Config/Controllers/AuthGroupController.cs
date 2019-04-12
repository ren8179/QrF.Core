using Microsoft.AspNetCore.Mvc;
using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using QrF.Core.Config.Interfaces;
using System;
using System.Threading.Tasks;

namespace QrF.Core.Config.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("ConfigAPI/[controller]")]
    [ApiController]
    public class AuthGroupController : ControllerBase
    {
        private readonly IAuthGroupBusiness _business;
        private readonly IReRouteGroupAuthBusiness _reRouteGroupbusiness;
        public AuthGroupController(IAuthGroupBusiness business, IReRouteGroupAuthBusiness reRouteGroupbusiness)
        {
            _business = business;
            _reRouteGroupbusiness = reRouteGroupbusiness;
        }
        /// <summary>
        /// 查询分页列表
        /// </summary>
        [HttpGet("GetPageList")]
        public async Task<IActionResult> GetPageListAsync([FromQuery] PageInput input)
        {
            var list = await _business.GetPageList(input);
            return Ok(list);
        }
        /// <summary>
        /// 分配路由
        /// </summary>
        [HttpPost("ToAccReRoute")]
        public async Task<IActionResult> ToAccReRouteAsync([FromBody] ToAccReRouteInput input)
        {
            var result = await _reRouteGroupbusiness.ToAccReRouteAsync(input);
            return Ok(new MsgResultDto { Success = result });
        }
        /// <summary>
        /// 编辑
        /// </summary>
        [HttpPost("Edit")]
        public async Task<IActionResult> EditAsync([FromBody] AuthGroup input)
        {
            await _business.EditModel(input);
            return Ok(new MsgResultDto { Success = true });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] DelInput input)
        {
            if (input == null || !input.Id.HasValue) throw new Exception("编号不存在");
            await _business.DelModel(input.Id.Value);
            return Ok(new MsgResultDto { Success = true });
        }
    }
}
