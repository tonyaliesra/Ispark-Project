// Konum: İspark.Repositories/NewsRepository.cs

using İspark.Datas;
using İspark.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace İspark.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _context;

        public NewsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<News>> GetAllAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<List<NewsListDto>> GetAllForListAsync()
        {
            return await _context.News
                .Select(n => new NewsListDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    ImageUrl = n.ImageUrl,
                    Desc1 = n.Desc1
                })
                .ToListAsync();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            try
            {
                bool canConnect = await _context.Database.CanConnectAsync();
                if (!canConnect)
                {
                    Console.WriteLine("CANNOT CONNECT TO DATABASE!");
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.ToString());
            }

            return await _context.News.FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}