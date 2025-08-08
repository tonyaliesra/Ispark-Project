
using İspark.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace İspark.Business
{
    public interface INewsService
    {
        Task<ServiceResult<News>> GetNewsDetailAsync(int id);
        Task<ServiceResult<List<NewsListDto>>> GetNewsListAsync();
        Task<ServiceResult<List<News>>> GetAllNewsAsync();
    }
}