using Microsoft.AspNetCore.Mvc;
using TeamTasks.BL.Interfaces;
using TeamTasks.Utils.Dtos;


namespace TeamTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _developerService.GetAll());
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            return Ok(await _developerService.GetAllActive());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _developerService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeveloperRequestDto developer)
        {
            var (success, message) = await _developerService.Create(developer);

            if (!success)
            {
                return BadRequest(new { Success = success, Message = message });
            }

            return Ok(new { Success = success, Message = message });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DeveloperRequestDto developer)
        {
            var resp = await _developerService.Update(id, developer);
            return Ok(new { Success = resp});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _developerService.Delete(id);
            return Ok(new { Success = resp });
        }
    }
}
