using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProjectTaskRepository
    {
        Task<ProjectTaskDto> AddProjectTask(ProjectTaskDto projectTask);
        Task<ProjectTaskDto> GetProjectTask(int ProjectTaskId);
        Task<IEnumerable<ProjectTaskDto>> GetAllProjectTasks();
        Task<ProjectTaskDto> UpdateProjectTask(ProjectTaskDto projectTask);
        Task<ProjectTaskDto> DeleteProjectTask(ProjectTaskDto projectTask);
        Task<ProjectDto> GetProject(int projectId);
    }
}
