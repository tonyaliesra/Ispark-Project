
using İspark.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace İspark.Business
{
    public interface ICampaignService
    {
        Task<ServiceResult<Campaign>> GetCampaignDetailAsync(int id);
        Task<ServiceResult<List<CampaignListDto>>> GetCampaignListAsync();
        Task<ServiceResult<List<Campaign>>> GetAllCampaignsAsync();
    }
}