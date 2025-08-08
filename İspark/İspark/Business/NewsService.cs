
using İspark.Model;
using İspark.Repositories;
using İspark.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace İspark.Business
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly LoggerService _loggerService;

        public NewsService(INewsRepository newsRepository, LoggerService loggerService)
        {
            _newsRepository = newsRepository;
            _loggerService = loggerService;
        }

        public async Task<ServiceResult<List<News>>> GetAllNewsAsync()
        {
            var news = await _newsRepository.GetAllAsync();
            _loggerService.LogNewsList(null, news); 
            return ServiceResult<List<News>>.Succeed(news);
        }

        public async Task<ServiceResult<List<NewsListDto>>> GetNewsListAsync()
        {
            var list = await _newsRepository.GetAllForListAsync();
            return ServiceResult<List<NewsListDto>>.Succeed(list);
        }

        public async Task<ServiceResult<News>> GetNewsDetailAsync(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
            {
                return ServiceResult<News>.Fail(404, ErrorCodes.NewsNotFound);
            }
            return ServiceResult<News>.Succeed(news);
        }
    }
}