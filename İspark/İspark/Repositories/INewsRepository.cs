
using İspark.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace İspark.Repositories
{
    public interface INewsRepository
    {
        Task<News> GetByIdAsync(int id);

        Task<List<News>> GetAllAsync();

        Task<List<NewsListDto>> GetAllForListAsync();
    }
}