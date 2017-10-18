using QrF.Core.Common.Extensions;
using QrF.Core.Domain.Exceptions;
using System;

namespace QrF.Core.Materials.Domain
{
    public class Material
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Spec { get; protected set; }
        public string Manufact { get; protected set; }
        public string Area { get; protected set; }
        public DateTime CreateTime { get; protected set; }
        public string CreateUser { get; protected set; }

        private Material() { }

        public Material(Guid matId, string name, string spec, string manufact, string area, string createUser)
        {
            Id = matId;
            CreateTime = DateTime.Now;
            SetName(name);
            SetSpec(spec);
            SetManufact(manufact);
            Area = area;
            CreateUser = createUser;
        }
        public void SetName(string name)
        {
            if (name.IsEmpty())
                throw new DomainException("name cannot be empty.");
            Name = name;
        }
        public void SetSpec(string spec)
        {
            if (spec.IsEmpty())
                throw new DomainException("spec cannot be empty.");
            Spec = spec;
        }
        public void SetManufact(string manufact)
        {
            if (manufact.IsEmpty())
                throw new DomainException("manufact cannot be empty.");
            Manufact = manufact;
        }
    }
}
