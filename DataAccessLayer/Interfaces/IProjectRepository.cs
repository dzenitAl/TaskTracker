using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Interfaces
{
    public interface IProjectRepository
    {
        Task<ProjectDto> AddProjectAsync(ProjectDto project);
        Task<ProjectDto> GetProjectAsync(int projectId);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task UpdateProjectAsync(int projectId, ProjectDto project);
        Task DeleteProjectAsync(int projectId);
        IEnumerable<ProjectDto> GetFilter(ProjectFilters search = null);

    }
}
