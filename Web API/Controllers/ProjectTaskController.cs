using System;
using System.Collections.Generic;
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
        public ProjectTaskController(IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetAllProjectTasksAsync()
        {
            try
            {
                return Ok(await _projectTaskService.GetAllProjectTasksAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetProjectTaskAsync(int id)
        {
            try
            {
                return Ok(await _projectTaskService.GetProjectTaskAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ProjectTask>> AddProjectTaskAsync(ProjectTask projectTask)
        {
            try
            {
                return Created("api/ProjectTask/Add", await _projectTaskService.AddProjectTaskAsync(projectTask));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteProjectTaskAsync(int id)
        {
            try
            {
                await _projectTaskService.DeleteProjectTaskAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateProjectTaskAsxnc(int id, ProjectTask projectTask)
        {
            try
            {
                await _projectTaskService.UpdateProjectTaskAsync(id, projectTask);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
