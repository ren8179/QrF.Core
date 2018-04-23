using QrF.ABP.Application.Services;
using QrF.ABP.Domain.Repositories;
using QrF.Core.Tasks.Application.Dtos;
using QrF.Core.Tasks.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QrF.Core.Tasks.Application
{
    public class ProjectTaskAppService : ApplicationService
    {
        private readonly IRepository<ProjectTask> _projectTaskRepository;
        public ProjectTaskAppService(IRepository<ProjectTask> projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<List<ProjectTaskDto>> GetAllAsync()
        {
            return ObjectMapper.Map<List<ProjectTaskDto>>(await _projectTaskRepository.GetAllListAsync());
        }

        public int CreateProjectTask(ProjectTaskCreateInput input)
        {
            input.Name = "";
            var product = ObjectMapper.Map<ProjectTask>(input);
            return _projectTaskRepository.InsertAndGetId(product);
        }

    }
}
