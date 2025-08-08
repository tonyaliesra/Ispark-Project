using İspark.Datas;
using İspark.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace İspark.Repositories
{
    
    public class CampaignRepository : ICampaignRepository
    {
        private readonly AppDbContext _context;

        public CampaignRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Campaign> GetByIdAsync(int id)
        {
            return await _context.Campaign.FirstOrDefaultAsync(k => k.Id == id);
        }

        
        public async Task<List<Campaign>> GetAllAsync()
        {
            return await _context.Campaign.ToListAsync();
        }

        // Kural 3'ü uygula:
        public async Task<List<CampaignListDto>> GetAllForListAsync()
        {
            return await _context.Campaign
                .Select(k => new CampaignListDto
                {
                    Id = k.Id,
                    Title = k.Title,
                    ImageUrl=k.ImageUrl,
                    Desc1 = k.Desc1
                })
                .ToListAsync();
        }
    }
}