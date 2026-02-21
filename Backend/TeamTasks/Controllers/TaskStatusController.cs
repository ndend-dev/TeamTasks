using Microsoft.AspNetCore.Mvc;
using TeamTasks.BL.Interfaces;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskStatusController : ControllerBase
    {
        private readonly ITaskStatusService _taskStatusService;

        public TaskStatusController(ITaskStatusService taskStatusService)
        {
            _taskStatusService = taskStatusService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _taskStatusService.GetAll());
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            return Ok(await _taskStatusService.GetAllActive());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _taskStatusService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TypeStatusRequestDto project)
        {
            var (success, message) = await _taskStatusService.Create(project);

            if (!success)
            {
                return BadRequest(new { Success = success, Message = message });
            }

            return Ok(new { Success = success, Message = message });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TypeStatusRequestDto priority)
        {
            var resp = await _taskStatusService.Update(id, priority);
            return Ok(new { Success = resp });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _taskStatusService.Delete(id);
            return Ok(new { Success = resp });
        }
    }
}
