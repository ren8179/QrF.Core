using QrF.Core.Infrastructure.Cqrs.Commands;
using QrF.Core.Materials.Domain;
using System.Threading.Tasks;

namespace QrF.Core.Materials.Services.Commands
{
    internal class CreateMaterialCommandHandler : ICommandHandler<CreateMaterialCommand>
    {
        private readonly IMaterialStorage _storage;

        public CreateMaterialCommandHandler(IMaterialStorage storage)
        {
            _storage = storage;
        }

        public async Task HandleAsync(CreateMaterialCommand command)
        {
            await _storage.AddAsync(
                new Material(command.Id, command.Name, command.Spec, command.Manufact,command.Area,command.CreateUser));
        }
    }
}
