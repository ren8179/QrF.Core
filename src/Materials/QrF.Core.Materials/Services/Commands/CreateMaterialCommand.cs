using QrF.Core.Infrastructure.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.Materials.Services.Commands
{
    public class CreateMaterialCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Manufact { get; set; }
        public string Area { get; set; }
        public string CreateUser { get; set; }
    }
}
