
using İspark.Business;
using İspark.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace İspark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getcampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        private readonly LoggerService _loggerService;

        public getcampaignController(ICampaignService campaignService, LoggerService loggerService)
        {
            _campaignService = campaignService;
            _loggerService = loggerService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetCampaignList()
        {
            var result = await _campaignService.GetCampaignListAsync();

            if (!result.Success)
            {
                return StatusCode(result.Error.StatusCode, result.Error);
            }

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampaignDetail(int id)
        {
            string username = User.Identity?.Name ?? "anonymous";

            var result = await _campaignService.GetCampaignDetailAsync(id);

            if (!result.Success)
            {
                _loggerService.LogError(result.Error.ErrorCode, $"User: {username}, Path: {Request.Path}");
                return StatusCode(result.Error.StatusCode, result.Error);
            }

            _loggerService.LogPageView(username, "Campaign", id, result.Data);
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCampaigns()
        {
            var result = await _campaignService.GetAllCampaignsAsync();

            if (!result.Success)
            {
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            return Ok(result.Data);
        }
    }
}