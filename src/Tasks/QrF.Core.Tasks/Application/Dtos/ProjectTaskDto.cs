using QrF.ABP.Application.Services.Dto;
using QrF.ABP.AutoMapper;
using QrF.Core.Tasks.Domain;

namespace QrF.Core.Tasks.Application.Dtos
{
    [AutoMap(typeof(ProjectTask))]
    public class ProjectTaskDto : EntityDto
    {
        public string Name { get; set; }

        public string Desc { get; set; }
    }
}
