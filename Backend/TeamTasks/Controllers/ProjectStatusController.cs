using Microsoft.AspNetCore.Mvc;
using TeamTasks.BL.Interfaces;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectStatusController : ControllerBase
    {
        private readonly IProjectStatusService _projectStatusService;

        public ProjectStatusController(IProjectStatusService projectStatusService)
        {
            _projectStatusService = projectStatusService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectStatusService.GetAll());
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            return Ok(await _projectStatusService.GetAllActive());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _projectStatusService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TypeStatusRequestDto project)
        {
            var (success, message) = await _projectStatusService.Create(project);

            if (!success)
            {
                return BadRequest(new { Success = success, Message = message });
            }

            return Ok(new { Success = success, Message = message });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TypeStatusRequestDto priority)
        {
            var resp = await _projectStatusService.Update(id, priority);
            return Ok(new { Success = resp });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _projectStatusService.Delete(id);
            return Ok(new { Success = resp });
        }
    }
}
