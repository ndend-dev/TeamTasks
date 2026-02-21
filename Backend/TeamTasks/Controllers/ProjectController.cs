using Microsoft.AspNetCore.Mvc;
using TeamTasks.BL.Interfaces;
using TeamTasks.BL.Services;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            this._projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _projectService.GetById(id));
        }

        [HttpGet("tasks")]
        public async Task<IActionResult> GetProjectTasks([FromQuery]ProjectTaskRequestDto projectTask)
        {
           
            if (projectTask.ProjectId == null)
                return BadRequest("Error: projectId es requerido.");

            return Ok(await _projectService.GetTaskByProject(projectTask));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRequestDto project)
        {
            var (success, message) = await _projectService.Create(project);

            if (!success)
            {
                return BadRequest(new { Success = success, Message = message });
            }

            return Ok(new { Success = success, Message = message });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ProjectRequestDto project)
        {
            var resp = await _projectService.Update(id, project);
            return Ok(new { Success = resp });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _projectService.Delete(id);
            return Ok(new { Success = resp });
        }

    }
}
