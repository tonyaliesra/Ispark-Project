
using İspark.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace İspark.Repositories
{
    public interface ICampaignRepository
    {
        Task<Campaign> GetByIdAsync(int id);

        Task<List<Campaign>> GetAllAsync();

        Task<List<CampaignListDto>> GetAllForListAsync();
    }
}