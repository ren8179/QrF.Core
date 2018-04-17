using System.Threading.Tasks;

namespace QrF.Core.Infrastructure.Cqrs.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
