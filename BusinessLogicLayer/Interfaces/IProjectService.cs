
using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProjectService
    {
        Task<Project> AddProject(Project project);
        Task<Project> GetProject(int projectId);
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> UpdateProject(Project project);
        Task<Project> DeleteProject(int projectId);
    }
}
