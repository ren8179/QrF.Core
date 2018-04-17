using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.Tasks.Domain
{
    public class Task
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Desc { get; protected set; }
        public string Manufact { get; protected set; }
        public string Area { get; protected set; }
        public DateTime CreateTime { get; protected set; }
        public string CreateUser { get; protected set; }

        private Task() { }

        public Task(Guid matId, string name, string spec, string manufact, string area, string createUser)
        {
            Id = matId;
            CreateTime = DateTime.Now;
            SetName(name);
            SetManufact(manufact);
            Area = area;
            CreateUser = createUser;
        }
        public void SetName(string name)
        {
            Name = name;
        }

        public void SetManufact(string manufact)
        {
            Manufact = manufact;
        }
    }
}
