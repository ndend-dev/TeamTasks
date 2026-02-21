using Microsoft.AspNetCore.Mvc;
using TeamTasks.BL.Interfaces;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskServices _taskServices;
        public TasksController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _taskServices.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _taskServices.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskRequestDto task)
        {

            var (success, message) = await _taskServices.Create(task);

            if (!success)
            {
                return BadRequest(new { Success = success, Message = message });
            }

            return Ok(new { Success = success, Message = message });

        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> Update(Guid id, TaskUpdateRequestDto task)
        {
            var resp = await _taskServices.Update(id, task);
            return Ok(new { Success = resp });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _taskServices.Delete(id);
            return Ok(new { Success = resp });
        }

        
    }
}
