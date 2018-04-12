using System;

namespace QrF.Core.Materials.Services.Dtos
{
    public class MaterialDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Manufact { get; set; }
        public string Area { get; set; }
        public string CreateUser { get; set; }
    }
}
