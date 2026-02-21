using Microsoft.AspNetCore.Mvc;
using TeamTasks.BL.Interfaces;
using TeamTasks.Utils.Dtos;

namespace TeamTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriorityController : Controller
    {
        private readonly IPriorityService _priorityService;

        public PriorityController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _priorityService.GetAll());
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            return Ok(await _priorityService.GetAllActive());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _priorityService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TypeStatusRequestDto priority)
        {
            var (success, message) = await _priorityService.Create(priority);

            if (!success)
            {
                return BadRequest(new { Success = success, Message = message });
            }

            return Ok(new { Success = success, Message = message });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TypeStatusRequestDto priority)
        {
            var resp = await _priorityService.Update(id, priority);
            return Ok(new { Success = resp });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _priorityService.Delete(id);
            return Ok(new { Success = resp });
        }
    }
}
