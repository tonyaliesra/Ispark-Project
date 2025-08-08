// Konum: İspark.Business/IUserService.cs

using İspark.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace İspark.Business
{
    /// <summary>
    /// Kimlik doğrulama işleminin sonucunu temsil eder.
    /// Başarılı ise Token, başarısız ise standart bir ErrorDetails nesnesi içerir.
    /// </summary>
    public class AuthResult
    {
        public bool Success { get; private set; }
        public string Token { get; private set; }
        public ErrorDetails Error { get; private set; }

        private AuthResult() { }

        public static AuthResult Succeed(string token)
        {
            return new AuthResult { Success = true, Token = token };
        }

        public static AuthResult Fail(int statusCode, int errorCode)
        {
            return new AuthResult
            {
                Success = false,
                Error = new ErrorDetails(statusCode, errorCode)
            };
        }
    }

    public interface IUserService
    {
        Task<AuthResult> LoginAsync(UserLoginDto loginDto);
        Task<List<Users>> GetAllUsersAsync();
    }
}