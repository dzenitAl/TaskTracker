using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IProjectTaskService _projectTaskService;
        public ProjectController(IProjectService projectService, IProjectTaskService projectTaskService)
        {
            _projectService = projectService;
            _projectTaskService = projectTaskService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjectsAsync()
        {
            try
            {
                return Ok(await _projectService.GetAllProjectsAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectAsync(int id)
        {
            try
            {
                var expectedProject = await _projectService.GetProjectAsync(id);

                if (expectedProject != null)
                {
                    return Ok(expectedProject);
                }

                return NotFound($"Project with Id: {id} was not found!");

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Project>> AddProjectAsync(Project project)
        {
            try
            {
                if (project == null)
                    return NotFound("Project was not found!");

                var newProject = await _projectService.AddProjectAsync(project);

                return newProject;
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occurred!");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task DeleteProjectAsync(int id)
        {
            try
            {
                var requestingProject = await _projectService.GetProjectAsync(id);
                if (requestingProject == null)
                {
                    NotFound("Project cannot be found!");
                }
                await _projectService.DeleteProjectAsync(id);
            }
            catch (Exception)
            {
                StatusCode(500, "An error has occurred!");
            }
            
        }

        [HttpPut("Update/{id}")]
        public async Task UpdateProjectAsxnc(int id, Project project)
        {
            try
            {
                if (project == null)
                    NotFound("Project was not found!");

                await _projectService.UpdateProjectAsync(id,project);
            }
            catch (Exception)
            {
                BadRequest();
            }
        }

        [HttpGet("GetFilter")]
        public ActionResult<IEnumerable<Project>> GetFilter([FromQuery] ProjectFilters search)
        {
            try
            {
                return Ok( _projectService.GetFilter(search));
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occurred!");
            }
        }

        [HttpPost("AddTask")]
        public async Task<ActionResult<ProjectTask>> AddProjectTaskAsync(ProjectTask projectTask)
        {
            try
            {
                if (projectTask == null)
                    return NotFound("Project task was not found!");

                var newProjectTask = await _projectTaskService.AddProjectTaskAsync(projectTask);

                return newProjectTask;
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occurred!");
            }
        }

        [HttpGet("GetAllTasks")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetAllProjectTasksAsync()
        {
            try
            {
                return Ok(await _projectTaskService.GetAllProjectTasksAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteTask/{id}")]
        public async Task DeleteProjectTaskAsync(int id)
        {
            try
            {
                var requestingProjectTask = await _projectTaskService.GetProjectTaskAsync(id);
                if (requestingProjectTask == null)
                {
                    NotFound("Project task cannot be found!");
                }
                await _projectTaskService.DeleteProjectTaskAsync(id);
            }
            catch (Exception)
            {
                StatusCode(500, "An error has occurred!");
            }
        }
    }
}
