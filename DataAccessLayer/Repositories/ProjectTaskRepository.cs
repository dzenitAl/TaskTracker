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

        public async Task<ProjectTaskDto> AddProjectTaskAsync(ProjectTaskDto projectTask)
        {
            await _context.ProjectTasks.AddAsync(projectTask);

            await _context.SaveChangesAsync();
            return projectTask;
        }

        public async Task<ProjectTaskDto> GetProjectTaskAsync(int projectTaskId)
        {
            return await _context.ProjectTasks.FindAsync(projectTaskId);
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetAllProjectTasksAsync()
        {
            return await _context.ProjectTasks.ToListAsync();
        }

        public async Task DeleteProjectTaskAsync(int projectTaskId)
        {
            var existingProjectTask = await _context.ProjectTasks.FindAsync(projectTaskId);

            _context.ProjectTasks.Remove(existingProjectTask);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectTaskAsync(int projectTaskId, ProjectTaskDto projectTask)
        {
            var existingProjectTask = await _context.ProjectTasks.FindAsync(projectTaskId);

            existingProjectTask.Name = projectTask.Name;
            existingProjectTask.Description = projectTask.Description;
            existingProjectTask.Status = projectTask.Status;
            existingProjectTask.Priority = projectTask.Priority;
            existingProjectTask.Status = projectTask.Status;

            await _context.SaveChangesAsync();
        } 
        
    }
}
