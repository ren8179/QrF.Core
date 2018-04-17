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
            Name = name;
        }
        public void SetSpec(string spec)
        {
            Spec = spec;
        }
        public void SetManufact(string manufact)
        {
            Manufact = manufact;
        }
    }
}
