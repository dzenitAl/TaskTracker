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
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTaskRepository _projectTaskRepository; 
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IProjectTaskRepository projectTaskRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        public async Task<Project>AddProjectAsync(Project project)
        {
            if (project == null)
                throw new Exception("Project not found!");

            var newProject = _mapper.Map<ProjectDto>(project);
                       
            await _projectRepository.AddProjectAsync(newProject);
            
            return _mapper.Map<Project>(newProject);
        }

        public async Task<Project> GetProjectAsync(int projectId)
        {
            var expectedProject = await _projectRepository.GetProjectAsync(projectId);

            if(expectedProject != null)
                return _mapper.Map<Project>(expectedProject);
            else
                throw new Exception($"Project with Id: {projectId} not found!");
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            var allProjects = await _projectRepository.GetAllProjectsAsync();

            var projects = new List<Project>();

            foreach (var project in allProjects)
            {
                projects.Add(_mapper.Map<Project>(project));
            }
            if (projects.Count() > 0)
                return projects;
            else
                throw new Exception("No Projects in the database!");
        }

        public async Task UpdateProjectAsync(int projectId, Project project)
        {
            if (project == null)
                throw new Exception("Project must be required!");
            
            var newProject = _mapper.Map<ProjectDto>(project);
            
            await _projectRepository.UpdateProjectAsync(projectId, newProject);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await GetProjectAsync(projectId);
            if (project == null)
                throw new Exception("Project not found!");
            
            await _projectRepository.DeleteProjectAsync(projectId);
        }

        public IEnumerable<Project> GetFilter(ProjectFilters search = null)
        {
            var result = _projectRepository.GetFilter(search);

            return _mapper.Map<IEnumerable<Project>>(result);
        }

        public async Task<ProjectVM> GetTasksOfProjectAsync(int projectId)
        {
            var project = await GetProjectAsync(projectId);

            var projectListTask = new ProjectVM()
            {
                Name = project.Name,
                StartDate = project.StartDate,
                CompletionDate = project.CompletionDate,
                Priority = project.Priority,
                Status = project.Status,
                Tasks = _mapper.Map<List<TaskVM>>(await _projectTaskRepository.GetListProjectTasksAsync(projectId))
            };

            return projectListTask;
        }
    }
}
