using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;

        public ProjectTaskService(IProjectTaskRepository projectTaskRepository, IMapper mapper)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        public async Task<ProjectTask> AddProjectTaskAsync(ProjectTask projectTask)
        {
            if (projectTask == null)
                throw new Exception("Project task must be required!");

            var newProjectTask = _mapper.Map<ProjectTaskDto>(projectTask);

            await _projectTaskRepository.AddProjectTaskAsync(newProjectTask);

            return _mapper.Map<ProjectTask>(newProjectTask);
        }

        public async Task<ProjectTask> GetProjectTaskAsync(int projectTaskId)
        {
            var expectedProjectTask = await _projectTaskRepository.GetProjectTaskAsync(projectTaskId);
           
            if(expectedProjectTask != null)
                return _mapper.Map<ProjectTask>(expectedProjectTask);
            else
                throw new Exception($"Project task with Id: {projectTaskId} not found!");
        }

        public async Task<IEnumerable<ProjectTask>> GetAllProjectTasksAsync()
        {
            var allProjectTasks = await _projectTaskRepository.GetAllProjectTasksAsync();
            var projectTasks = new List<ProjectTask>();

            foreach (var projectTask in allProjectTasks)
            {
                projectTasks.Add(_mapper.Map<ProjectTask>(projectTask));
            }
            if (projectTasks.Count() > 0)
                return projectTasks;
            else
                throw new Exception("No Project Tasks in the database!");
        }

        public async Task UpdateProjectTaskAsync(int projectTaskId, ProjectTask projectTask)
        {
            if (projectTask == null)
                throw new Exception("Project task not found!");

            var newProjectTask = _mapper.Map<ProjectTaskDto>(projectTask);

            await _projectTaskRepository.UpdateProjectTaskAsync(projectTaskId, newProjectTask);
        }

        public async Task DeleteProjectTaskAsync(int projectTaskId)
        {
            var projectTask = await GetProjectTaskAsync(projectTaskId);
            if(projectTask == null)
                throw new Exception("Project task not found!");

            await _projectTaskRepository.DeleteProjectTaskAsync(projectTaskId);
        }
    }
}
