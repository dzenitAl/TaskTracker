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
        private readonly TaskTrackerDbContext _context;
        private readonly IProjectRepository _projectRepository;
        protected readonly IMapper _mapper;
        public ProjectService(TaskTrackerDbContext context, IProjectRepository projectRepository ,IMapper mapper)
        {
            _context = context;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<Project>AddProject(Project project)
        {
            var newProject = _mapper.Map<ProjectDto>(project);

            await _projectRepository.AddProject(newProject);
            await _context.SaveChangesAsync();

            return _mapper.Map<Project>(newProject);
        }

        public async Task<Project> GetProject(int projectId)
        {
            var expectedProject = await _projectRepository.GetProject(projectId);
            return _mapper.Map<Project>(expectedProject);
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            var allProjects = await _projectRepository.GetAllProjects();
            return (IEnumerable<Project>)_mapper.Map<Project>(allProjects);
        }

        public async Task<Project> UpdateProject(Project project)
        {
            var existingProject = _mapper.Map<ProjectDto>(project);

            return _mapper.Map<Project>(await _projectRepository.UpdateProject(existingProject));
        }
        public async Task<Project> DeleteProject(int projectId)
        {
            var expectedProject = await _projectRepository.GetProject(projectId);

            await _projectRepository.DeleteProject(expectedProject);
            await _context.SaveChangesAsync();
            return _mapper.Map<Project>(expectedProject);
        }

    }
}
