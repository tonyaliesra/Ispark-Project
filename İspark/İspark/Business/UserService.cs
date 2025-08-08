using İspark.Repositories;
using İspark.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using İspark.Services;
using Microsoft.AspNetCore.Http;

namespace İspark.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly LoggerService _loggerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(
            IUserRepository userRepository,
            IConfiguration configuration,
            LoggerService loggerService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _loggerService = loggerService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<AuthResult> LoginAsync(UserLoginDto loginDto)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            string endpoint = $"{request.Method} {request.Path}";

            if (string.IsNullOrWhiteSpace(loginDto.Username))
            {
                _loggerService.LogLoginAttempt(loginDto.Username ?? "empty", ErrorCodes.UserNameEmpty, endpoint);
                return AuthResult.Fail(400, ErrorCodes.UserNameEmpty);
            }
            if (string.IsNullOrWhiteSpace(loginDto.Password))
            {
                _loggerService.LogLoginAttempt(loginDto.Username, ErrorCodes.PasswordEmpty, endpoint);
                return AuthResult.Fail(400, ErrorCodes.PasswordEmpty);
            }

            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);

           
            if (user != null)
            {
                
                if (user.Password != loginDto.Password)
                {
                    _loggerService.LogLoginAttempt(loginDto.Username, ErrorCodes.PasswordWrong, endpoint);
                    return AuthResult.Fail(401, ErrorCodes.PasswordWrong);
                }
            }
            else
            {
                
                bool isPasswordCorrectForSomeoneElse = await _userRepository.IsPasswordUsedByAnyUserAsync(loginDto.Password);

                if (isPasswordCorrectForSomeoneElse)
                {
                    
                    _loggerService.LogLoginAttempt(loginDto.Username, ErrorCodes.UserNameWrong, endpoint);
                    return AuthResult.Fail(401, ErrorCodes.UserNameWrong);
                }
                else
                {
                   
                    _loggerService.LogLoginAttempt(loginDto.Username, ErrorCodes.UserNotFound, endpoint);
                    return AuthResult.Fail(401, ErrorCodes.UserNotFound);
                }
            }
            var tokenString = GenerateJwtToken(user);
            string maskedToken = tokenString.Length > 10 ? tokenString.Substring(0, 5) + "..." + tokenString.Substring(tokenString.Length - 5) : tokenString;
            _loggerService.LogLoginAttempt(user.Username, 0, endpoint, maskedToken);
            return AuthResult.Succeed(tokenString);
        }

        private string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = _configuration["Jwt:Key"];
            var key = Encoding.ASCII.GetBytes(jwtKey!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}