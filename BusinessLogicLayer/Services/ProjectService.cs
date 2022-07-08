using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.EF;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var newProject = _mapper.Map<ProjectDto>(project);
            await _projectRepository.AddProjectAsync(newProject);
            
            return _mapper.Map<Project>(newProject);
        }

        public async Task<Project> GetProjectAsync(int projectId)
        {
            var expectedProject = await _projectRepository.GetProjectAsync(projectId);
            return _mapper.Map<Project>(expectedProject);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            var allProjects = await _projectRepository.GetAllProjectsAsync();
            var projects = new List<Project>();

            foreach (var project in allProjects)
            {
                projects.Add(_mapper.Map<Project>(project));
            }
            return projects;
        }

        public async Task UpdateProjectAsync(int projectId, Project project)
        {
            var newProject = _mapper.Map<ProjectDto>(project);
            
            _mapper.Map<ProjectDto>(_projectRepository.UpdateProjectAsync(projectId, newProject));
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            await _projectRepository.DeleteProjectAsync(projectId);
        }

    }
}
