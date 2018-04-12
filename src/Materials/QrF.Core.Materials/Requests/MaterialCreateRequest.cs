using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.Materials.Requests
{
    public class MaterialCreateRequest : IRequest<string>
    {
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Manufact { get; set; }
        public string Area { get; set; }
        public string CreateTime { get; set; }
    }
}
