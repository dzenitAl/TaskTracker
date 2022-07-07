using DataAccessLayer.EF;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<ProjectDto> AddProject(ProjectDto project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<ProjectDto> GetProject(int projectId)
        {
            return await _context.Projects.FindAsync(projectId);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<ProjectDto> DeleteProject(ProjectDto project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<ProjectDto> UpdateProject(ProjectDto project)
        {
            var existingProject = await _context.Projects.FindAsync(project.Id);

            existingProject.Name = project.Name;
            existingProject.StartDate = project.StartDate;
            existingProject.CompletionDate = project.CompletionDate;
            existingProject.Priority = project.Priority;
            existingProject.Status = project.Status;

            await _context.SaveChangesAsync();
            return project;
        }

    }
}
