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
            return Ok(await _projectService.GetAllProjectsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectAsync(int id)
        {
            var expectedProject = await _projectService.GetProjectAsync(id);
            
            if(expectedProject != null)
            {
                return Ok(expectedProject);
            }

            return NotFound($"Project with Id: {id} was not found");
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Project>> AddProjectAsync(Project project)
        {
            var newProject = await _projectService.AddProjectAsync(project);

            return newProject;
        }

        [HttpDelete("Delete/{id}")]
        public async Task DeleteProjectAsync(int id)
        {
            var requestingProject = await _projectService.GetProjectAsync(id);
            if(requestingProject == null)
            {
                NotFound("Projects cannot be found");
            }
            await _projectService.DeleteProjectAsync(id);
        }

        [HttpPut("Update/{id}")]
        public async Task UpdateProjectAsxnc(int id, Project project)
        {
            await _projectService.UpdateProjectAsync(id,project);
        }
    }
}
