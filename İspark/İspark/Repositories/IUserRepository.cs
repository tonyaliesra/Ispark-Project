using İspark.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace İspark.Repositories
{
    public interface IUserRepository
    {
        Task<Users> GetByUsernameAsync(string username);
        Task<List<Users>> GetAllAsync();

        Task<bool> IsPasswordUsedByAnyUserAsync(string password);
    }
}