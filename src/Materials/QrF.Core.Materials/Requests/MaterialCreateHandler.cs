using MediatR;
using QrF.Core.Materials.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QrF.Core.Materials.Requests
{
    public class MaterialCreateHandler : IRequestHandler<MaterialCreateRequest, string>
    {
        private readonly IMaterialStorage _storage;

        public MaterialCreateHandler(IMaterialStorage storage)
        {
            _storage = storage;
        }

        public async Task<string> Handle(MaterialCreateRequest request, CancellationToken cancellationToken)
        {
            Guid newUserId = Guid.NewGuid();
            await _storage.AddAsync(new Material(newUserId, request.Name, request.Spec, request.Manufact,request.Area,request.CreateTime));
            return newUserId.ToString();
        }
    }
}
