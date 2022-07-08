using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProjectTaskService
    {
        Task<ProjectTask> AddProjectTaskAsync(ProjectTask projectTask);
        Task<ProjectTask> GetProjectTaskAsync(int projectTaskId);
        Task<IEnumerable<ProjectTask>> GetAllProjectTasksAsync();
        Task UpdateProjectTaskAsync(int projectTaskId, ProjectTask projectTask);
        Task DeleteProjectTaskAsync(int projectTaskId);
    }
}
