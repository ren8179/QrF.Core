using MediatR;
using Microsoft.AspNetCore.Mvc;
using QrF.Core.API.Infrastructure;
using QrF.Core.Infrastructure.Cqrs.Commands;
using QrF.Core.Infrastructure.Cqrs.Queries;
using QrF.Core.Materials.Services.Commands;
using QrF.Core.Materials.Services.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.API.V1.Controllers
{
    /// <summary>
    /// 物品管理
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MaterialController : BaseController
    {
        public MaterialController(ICommandDispatcher commandDispatcher, IQueryExecutor queryExecutor,
            IMediator mediator) : base(commandDispatcher, queryExecutor, mediator) { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await ExecuteAsync(new GetAllQuery());
            if (!users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }

        [Route("{id}", Name = "GetUserById"), HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await ExecuteAsync(new GetForIdQuery(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMaterialCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }
            await DispatchAsync(command);
            return CreatedAtRoute("GetUserById", new { id = command.Id }, command);
        }
    }
}
