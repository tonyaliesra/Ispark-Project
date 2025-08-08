
using İspark.Model;
using İspark.Repositories;
using İspark.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace İspark.Business
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly LoggerService _loggerService;

        public CampaignService(ICampaignRepository campaignRepository, LoggerService loggerService)
        {
            _campaignRepository = campaignRepository;
            _loggerService = loggerService;
        }

        public async Task<ServiceResult<List<Campaign>>> GetAllCampaignsAsync()
        {
            var campaigns = await _campaignRepository.GetAllAsync();
            _loggerService.LogCampaignList(null, campaigns); 
            return ServiceResult<List<Campaign>>.Succeed(campaigns);
        }

        public async Task<ServiceResult<List<CampaignListDto>>> GetCampaignListAsync()
        {
            var list = await _campaignRepository.GetAllForListAsync();
            return ServiceResult<List<CampaignListDto>>.Succeed(list);
        }

        public async Task<ServiceResult<Campaign>> GetCampaignDetailAsync(int id)
        {
            var campaign = await _campaignRepository.GetByIdAsync(id);
            if (campaign == null)
            {
                return ServiceResult<Campaign>.Fail(404, ErrorCodes.CampaignNotFound);
            }
            return ServiceResult<Campaign>.Succeed(campaign);
        }
    }
}