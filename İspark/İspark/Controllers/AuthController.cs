
using İspark.Business;
using İspark.Model;
using Microsoft.AspNetCore.Mvc;
using System.IO; 
using System.Threading.Tasks;

namespace İspark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginUser)
        {
            var result = await _userService.LoginAsync(loginUser);

            if (!result.Success)
            {
                return StatusCode(result.Error.StatusCode, result.Error);
            }

            return Ok(new { message = "Login successful", token = result.Token });
        }

        [HttpGet("logs/{date}")]
        public IActionResult DownloadLog(string date)
        {
            if (string.IsNullOrWhiteSpace(date) || date.Contains("..") || date.Contains("/") || date.Contains("\\"))
            {
                return BadRequest(new ErrorDetails(400, ErrorCodes.IdFormatInvalid));
            }

            string filePath = Path.Combine("Logs", $"log-{date}.txt"); 

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new ErrorDetails(404, ErrorCodes.NotFound));
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "text/plain", Path.GetFileName(filePath));
        }
    }
}