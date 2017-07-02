using System.Threading.Tasks;
using FoxMoney.Server.Services;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoxMoney.Server.Controllers.api
{
    //[Authorize]
    [Route("api/[controller]")]
    public class AdminController : BaseController
    {
        [HttpPost("ProcessEndDay")]
        public async Task<IActionResult> ProcessEndDayUpdates() {
            BackgroundJob.Enqueue<BackgroundProcessingService>(service => service.UpdateEndOfDayValues());

            return Ok();
        }
    }
}