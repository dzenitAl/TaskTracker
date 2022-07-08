using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProjectTaskRepository
    {
        Task<ProjectTaskDto> AddProjectTaskAsync(ProjectTaskDto projectTask);
        Task<ProjectTaskDto> GetProjectTaskAsync(int projectTaskId);
        Task<IEnumerable<ProjectTaskDto>> GetAllProjectTasksAsync();
        Task UpdateProjectTaskAsync(int projectTaskId, ProjectTaskDto projectTask);
        Task DeleteProjectTaskAsync(int projectTaskId);
    }
}
