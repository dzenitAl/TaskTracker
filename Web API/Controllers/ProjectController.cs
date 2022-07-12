using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjectsAsync()
        {
            try
            {
                return Ok(await _projectService.GetAllProjectsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectAsync(int id)
        {
            try
            {
                return  await _projectService.GetProjectAsync(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Project>> AddProjectAsync(Project project)
        {
            try
            {
                return await _projectService.AddProjectAsync(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteProjectAsync(int id)
        {
            try
            {
                await _projectService.DeleteProjectAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateProjectAsxnc(int id, Project project)
        {
            try
            {
                await _projectService.UpdateProjectAsync(id,project);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetFilter")]
        public ActionResult<IEnumerable<Project>> GetFilter([FromQuery] ProjectFilters search)
        {
            try
            {
                return Ok( _projectService.GetFilter(search));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
