using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.EF;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly TaskTrackerDbContext _context;
        private readonly IProjectTaskRepository _projectTaskRepository;
        protected readonly IMapper _mapper;
        public ProjectTaskService(TaskTrackerDbContext context, IProjectTaskRepository projectTaskRepository, IMapper mapper)
        {
            _context = context;
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        public async Task<ProjectTask> AddProjectTask(ProjectTask projectTask)
        {
            var newProjectTask = _mapper.Map<ProjectTaskDto>(projectTask);

            await _projectTaskRepository.AddProjectTask(newProjectTask);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectTask>(newProjectTask);
        }

        public async Task<ProjectTask> GetProjectTask(int ProjectTaskId)
        {
            var expectedProjectTask = await _projectTaskRepository.GetProjectTask(ProjectTaskId);
            return _mapper.Map<ProjectTask>(expectedProjectTask);
        }

        public async Task<IEnumerable<ProjectTask>> GetAllProjectTasks()
        {
            var allProjectTasks = await _projectTaskRepository.GetAllProjectTasks();
            return (IEnumerable<ProjectTask>)_mapper.Map<ProjectTask>(allProjectTasks);
        }

        public async Task<ProjectTask> UpdateProjectTask(ProjectTask projectTask)
        {
            var existingProjectTask = _mapper.Map<ProjectTaskDto>(projectTask);

            return _mapper.Map<ProjectTask>(await _projectTaskRepository.UpdateProjectTask(existingProjectTask));
        }
        public async Task<ProjectTask> DeleteProjectTask(int projectTaskId)
        {
            var expectedProjectTask = await _projectTaskRepository.GetProjectTask(projectTaskId);

            await _projectTaskRepository.DeleteProjectTask(expectedProjectTask);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectTask>(expectedProjectTask);
        }

        public async Task<Project> GetProject(int projectId)
        {
            var expectedProject = await _projectTaskRepository.GetProject(projectId);
            return _mapper.Map<Project>(expectedProject);
        }
    }
}
