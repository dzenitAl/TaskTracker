using BusinessLogicLayer.Entities;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProjectService
    {
        Task<Project> AddProjectAsync(Project project);
        Task<Project> GetProjectAsync(int projectId);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task UpdateProjectAsync(int projectId, Project project);
        Task DeleteProjectAsync(int projectId);
        IEnumerable<Project> GetFilter(ProjectFilters search = null);
    }
}
