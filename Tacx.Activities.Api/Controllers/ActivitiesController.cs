using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tacx.Activities.Api.Contracts;
using Tacx.Activities.Api.Core.Services.Activities;

namespace Tacx.Activities.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActivityPostRequest request)
        {
            var activity = await _activityService.Create(ActivityRequestMapper.Map(request));
            return Ok(ActivityRequestMapper.Map(activity));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(string id)
        {
            var activity = await _activityService.GetById(id);
            return Ok(ActivityRequestMapper.Map(activity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(string id)
        {
            await _activityService.Delete(id);
            return Ok();
        }
    }
}