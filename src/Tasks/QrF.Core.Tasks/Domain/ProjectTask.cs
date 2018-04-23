using QrF.ABP.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QrF.Core.Tasks.Domain
{
    [Table("ProjectTasks")]
    public class ProjectTask : Entity
    {
        public string Name { get; protected set; }
        public string Desc { get; protected set; }
        public DateTime CreateTime { get; protected set; }
        public string CreateUser { get; protected set; }

        private ProjectTask() { }

        public ProjectTask(string name, string desc,string createUser)
        {
            CreateTime = DateTime.Now;
            Name = name;
            Desc = desc;
            CreateUser = createUser;
        }

    }
}
