using DataAccessLayer.EF;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskTrackerDbContext _context;
        public ProjectRepository(TaskTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectDto> AddProjectAsync(ProjectDto project)
        {
            await _context.Projects.AddAsync(project);

            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<ProjectDto> GetProjectAsync(int projectId)
        {
            return await _context.Projects.FindAsync(projectId);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var existingProject = await _context.Projects.FindAsync(projectId);

            _context.Projects.Remove(existingProject);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(int projectId, ProjectDto project)
        {
            var existingProject = await _context.Projects.FindAsync(projectId);
            if (existingProject != null)
            {
                existingProject.Name = project.Name;
                existingProject.StartDate = project.StartDate;
                existingProject.CompletionDate = project.CompletionDate;
                existingProject.Priority = project.Priority;
                existingProject.Status = project.Status;

                await _context.SaveChangesAsync();
            }
            else
                throw new Exception($"Project with Id: {projectId} doesn't exist!");
        }

        public IEnumerable<ProjectDto> GetFilter(ProjectFilters search = null)
        {
            var entity = _context.Projects.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Name))
            {
                entity = entity.Where(x => x.Name.Contains(search.Name));
            }

            if (search.StartDate.HasValue)
            {
                entity = entity.Where(x => x.StartDate.Value == search.StartDate.Value);
            }

            if (search.CompletionDate.HasValue)
            {
                entity = entity.Where(x => x.CompletionDate == search.StartDate);
            }

            if (search.Priority.HasValue)
            {
                entity = entity.Where(x => x.Priority >= search.Priority);
            }


            var filterList = new List<ProjectDto>();

            foreach (var project in entity)
            {
                filterList.Add(project);
            }
            return filterList;
        }
    }
}
