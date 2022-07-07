using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProjectTaskService
    {
        Task<ProjectTask> AddProjectTask(ProjectTask projectTask);
        Task<ProjectTask> GetProjectTask(int ProjectTaskId);
        Task<IEnumerable<ProjectTask>> GetAllProjectTasks();
        Task<ProjectTask> UpdateProjectTask(ProjectTask projectTask);
        Task<ProjectTask> DeleteProjectTask(int projectTaskId);
        Task<Project> GetProject(int projectId);
    }
}
