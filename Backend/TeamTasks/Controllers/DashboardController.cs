using Microsoft.AspNetCore.Mvc;
using TeamTasks.BL.Interfaces;

namespace TeamTasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ITaskServices _taskServices;
        public DashboardController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        [HttpGet("developer-workload")]
        public async Task<IActionResult> GetDeveloperWorkload()
        {
            return Ok(await _taskServices.GetDeveloperWorkload());
        }

        [HttpGet("project-health")]
        public async Task<IActionResult> GetProjectStatusSummary()
        {
            return Ok(await _taskServices.GetProjectStatusSummary());
        }

        [HttpGet("upcoming-deadlines")]
        public async Task<IActionResult> GetUpcomingDeadlines()
        {
            return Ok(await _taskServices.GetUpcomingDeadlines());
        }

        [HttpGet("developer-delay-risk")]
        public async Task<IActionResult> GetDeveloperDelayRickPrediction()
        {
            return Ok(await _taskServices.GetDeveloperDelayRickPrediction());
        }
    }
}
