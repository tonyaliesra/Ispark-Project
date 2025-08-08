

using İspark.Business;
using İspark.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace İspark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getnewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly LoggerService _loggerService;

        public getnewsController(INewsService newsService, LoggerService loggerService)
        {
            _newsService = newsService;
            _loggerService = loggerService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetNewsList()
        {
            var result = await _newsService.GetNewsListAsync();

            if (!result.Success)
            {
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsDetail(int id)
        {
            string username = User.Identity?.Name ?? "anonymous";

            var result = await _newsService.GetNewsDetailAsync(id);

            if (!result.Success)
            {
                _loggerService.LogError(result.Error.ErrorCode, $"User: {username}, Path: {Request.Path}");
                return StatusCode(result.Error.StatusCode, result.Error);
            }

            _loggerService.LogPageView(username, "News", id, result.Data);
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var result = await _newsService.GetAllNewsAsync();

            if (!result.Success)
            {
                return StatusCode(result.Error.StatusCode, result.Error);
            }
            return Ok(result.Data);
        }
    }
}