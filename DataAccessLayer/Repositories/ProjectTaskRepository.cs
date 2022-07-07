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
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly TaskTrackerDbContext _context;
        public ProjectTaskRepository(TaskTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectTaskDto> AddProjectTask(ProjectTaskDto projectTask)
        {
            await _context.ProjectTasks.AddAsync(projectTask);
            await _context.SaveChangesAsync();
            return projectTask;
        }

        public async Task<ProjectTaskDto> GetProjectTask(int ProjectTaskId)
        {
            return await _context.ProjectTasks.FindAsync(ProjectTaskId);
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetAllProjectTasks()
        {
            return await _context.ProjectTasks.ToListAsync();
        }

        public async Task<ProjectTaskDto> DeleteProjectTask(ProjectTaskDto projectTask)
        {
            _context.ProjectTasks.Remove(projectTask);
            await _context.SaveChangesAsync();
            return projectTask;
        }

        public async Task<ProjectTaskDto> UpdateProjectTask(ProjectTaskDto projectTask)
        {
            var existingProjectTask = await _context.ProjectTasks.FindAsync(projectTask.Id);

            existingProjectTask.Name = projectTask.Name;
            existingProjectTask.Description = projectTask.Description;
            existingProjectTask.Status = projectTask.Status;
            existingProjectTask.Priority = projectTask.Priority;
            existingProjectTask.Status = projectTask.Status;
            existingProjectTask.ProjectId = projectTask.ProjectId;

            await _context.SaveChangesAsync();
            return projectTask;
        }

        public async Task<ProjectDto> GetProject(int projectId)
        {
            var expectedProject = await _context.Projects.FindAsync(projectId);
            return expectedProject;
        }

    }
}
