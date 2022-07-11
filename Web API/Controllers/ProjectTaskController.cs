using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly IProjectService _projectService;
        public ProjectTaskController(IProjectTaskService projectTaskService, IProjectService projectService)
        {
            _projectTaskService = projectTaskService;
            _projectService = projectService;
        }

        [HttpGet("GetAll")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetProjectTaskAsync(int id)
        {
            try
            {
                var expectedProjectTask = await _projectTaskService.GetProjectTaskAsync(id);

                if(expectedProjectTask == null)
                    return NotFound($"Project task with Id: {id} was not found!");

                return Ok(expectedProjectTask);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ProjectTask>> AddProjectTaskAsync(ProjectTask projectTask)
        {
            try
            {
                if (projectTask == null)
                    return NotFound("Project task was not found!");

                var project = _projectService.GetProjectAsync(projectTask.ProjectId);
                if (project == null)
                {
                    return NotFound($"Project with Id = {projectTask.ProjectId} not found!");
                }

                var newProjectTask = await _projectTaskService.AddProjectTaskAsync(projectTask);

                return newProjectTask;
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occurred!");
            }
        }

        [HttpDelete("Delete/{id}")]
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

        [HttpPut("Update/{id}")]
        public async Task UpdateProjectTaskAsxnc(int id, ProjectTask projectTask)
        {
            try
            {
                if (projectTask == null)
                    NotFound("Project task was not found!");

                await _projectTaskService.UpdateProjectTaskAsync(id, projectTask);
            }
            catch (Exception)
            {
                BadRequest();
            }
        }

    }
}
