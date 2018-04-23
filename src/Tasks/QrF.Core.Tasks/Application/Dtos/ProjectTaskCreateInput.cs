using QrF.ABP.AutoMapper;
using QrF.Core.Tasks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.Tasks.Application.Dtos
{
    [AutoMapTo(typeof(ProjectTask))]
    public class ProjectTaskCreateInput
    {
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
