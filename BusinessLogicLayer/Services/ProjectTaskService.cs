using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.EF;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectTaskService(IProjectTaskRepository projectTaskRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _projectTaskRepository = projectTaskRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectTask> AddProjectTaskAsync(ProjectTask projectTask)
        {
            var newProjectTask = _mapper.Map<ProjectTaskDto>(projectTask);

            await _projectTaskRepository.AddProjectTaskAsync(newProjectTask);

            return _mapper.Map<ProjectTask>(newProjectTask);
        }

        public async Task<ProjectTask> GetProjectTaskAsync(int projectTaskId)
        {
            var expectedProjectTask = await _projectTaskRepository.GetProjectTaskAsync(projectTaskId);
            return _mapper.Map<ProjectTask>(expectedProjectTask);
        }

        public async Task<IEnumerable<ProjectTask>> GetAllProjectTasksAsync()
        {
            var allProjectTasks = await _projectTaskRepository.GetAllProjectTasksAsync();
            var projectTasks = new List<ProjectTask>();

            foreach (var projectTask in allProjectTasks)
            {
                projectTasks.Add(_mapper.Map<ProjectTask>(projectTask));
            }
            return projectTasks;
        }

        public async Task UpdateProjectTaskAsync(int projectTaskId, ProjectTask projectTask)
        {
            var newProjectTask = _mapper.Map<ProjectTaskDto>(projectTask);

            _mapper.Map<ProjectTask>( _projectTaskRepository.UpdateProjectTaskAsync(projectTaskId, newProjectTask)); 
        }

        public async Task DeleteProjectTaskAsync(int projectTaskId)
        {
            await _projectTaskRepository.DeleteProjectTaskAsync(projectTaskId);
        }

    }
}
