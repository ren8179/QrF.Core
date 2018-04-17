using System.Threading.Tasks;

namespace QrF.Core.Infrastructure.Cqrs.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
