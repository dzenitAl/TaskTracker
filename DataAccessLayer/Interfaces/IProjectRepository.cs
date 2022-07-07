using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProjectRepository
    {
        Task<ProjectDto> AddProject(ProjectDto project);
        Task<ProjectDto> GetProject(int projectId);
        Task<IEnumerable<ProjectDto>> GetAllProjects();
        Task<ProjectDto> UpdateProject(ProjectDto project);
        Task<ProjectDto> DeleteProject(ProjectDto project);
    }
}
