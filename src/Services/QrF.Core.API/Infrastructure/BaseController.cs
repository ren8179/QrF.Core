using MediatR;
using Microsoft.AspNetCore.Mvc;
using QrF.Core.Infrastructure.Cqrs.Commands;
using QrF.Core.Infrastructure.Cqrs.Queries;
using System.Threading.Tasks;

namespace QrF.Core.API.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseController : Controller
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IMediator _mediator;
        private readonly ICommandDispatcher _commandDispatcher;

        protected BaseController(ICommandDispatcher commandDispatcher, IQueryExecutor queryExecutor,
            IMediator mediator)
        {
            _mediator = mediator;
            _queryExecutor = queryExecutor;
            _commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            await _commandDispatcher.DispatchAsync(command);
        }

        protected async Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query)
        {
            return await _queryExecutor.ExecuteAsync<IQuery<TResult>, TResult>(query);
        }

        protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return await _mediator.Send(request);
        }
    }
}
