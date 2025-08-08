// Konum: İspark.Services/LoggerService.cs

using İspark.Model;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq; // LINQ için eklendi.
using System.Text;

namespace İspark.Services
{
    public class LoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void LogLoginAttempt(string username, int errorCode, string endpoint, string? maskedToken = null)
        {
        
            if (errorCode == 0)
            {
                string tokenInfo = maskedToken != null ? $" - Token: {maskedToken}" : "";
                _logger.LogInformation("[INFO] User: {Username} → {Endpoint} → Status: SUCCESSFUL{TokenInfo}",
                    username, endpoint, tokenInfo);
                return;
            }

            string statusMessage = ErrorMessages.Messages.GetValueOrDefault(errorCode, "UNKNOWN_ERROR");

            _logger.LogError("[ERROR {ErrorCode}] User: {Username} → {Endpoint} → Status: {StatusMessage}",
                errorCode, username, endpoint, statusMessage);
        }
       
        public void LogPageView(string username, string pageType, int contentId)
        {
            _logger.LogInformation("[INFO] {PageType} viewed. → User: {Username} → ID: {ContentId}",
                                   pageType, username, contentId);
        }

        public void LogNotFound(string username, string pageType, int contentId, string requestUrl)
        {
            _logger.LogWarning("[ERROR] {PageType} not found. → User: {Username} → {Url} → {PageType} ID: {ContentId}",
                               pageType, username, requestUrl, pageType, contentId);
        }

        // Kampanya detay logu
        public void LogPageView(string username, string entityName, int id, object data)
        {
            string title = data?.GetType().GetProperty("Title")?.GetValue(data)?.ToString() ?? "-";
            string desc1 = data?.GetType().GetProperty("Desc1")?.GetValue(data)?.ToString() ?? "-";
            string desc2 = data?.GetType().GetProperty("Desc2")?.GetValue(data)?.ToString() ?? "-";
            string imageURL = data?.GetType().GetProperty("ImageUrl")?.GetValue(data)?.ToString() ?? "-";
            string kstart = data?.GetType().GetProperty("StartDate")?.GetValue(data)?.ToString() ?? "-";
            string kend = data?.GetType().GetProperty("EndDate")?.GetValue(data)?.ToString()?? "-";

            _logger.LogInformation("[INFO] {EntityName} detail viewed. → User: {Username} | ID: {Id} | Title: \"{Title}\" | Desc1: \"{Desc1}\" | Desc2: \"{Desc2}\" | Image: {ImageURL} | Start: {Kstart} | End: {Kend}",
                                   entityName, username, id, title, desc1, desc2, imageURL, kstart, kend);
        }

        // Haber detay logu
        public void LogPageView(string username, string entityName, int id, İspark.Model.News data)
        {
            string title = data?.Title ?? "-";
            string desc1 = data?.Desc1?? "-";
            string desc2 = data?.Desc2?? "-";
            string imageUrl = data?.ImageUrl ?? "-";

            _logger.LogInformation("[INFO] {EntityName} detail viewed. → User: {Username} | ID: {Id} | Title: \"{Title}\" | Desc1: \"{Desc1}\" | Desc2: \"{Desc2}\" | Image: {ImageUrl}",
                                   entityName, username, id, title, desc1, desc2, imageUrl);
        }

        public void LogNewsList(string username, List<News> newsList)
        {
            if (newsList == null || newsList.Count == 0)
            {
                _logger.LogInformation("[INFO] News list is empty.{UserInfo}",
                    string.IsNullOrEmpty(username) ? "" : $" User: {username}");
                return;
            }

            foreach (var news in newsList)
            {
                string userInfo = string.IsNullOrEmpty(username) ? "" : $" | User: {username}";
                string image = string.IsNullOrWhiteSpace(news.ImageUrl) ? "-" : news.ImageUrl;

                _logger.LogInformation("[INFO] News → ID: {Id} | Title: \"{Title}\" | Desc1: \"{Desc1}\" | Desc2: \"{Desc2}\" | Image: {ImageUrl}{UserInfo}",
                    news.Id, news.Title ?? "-", news.Desc1 ?? "-", news.Desc2 ?? "-", image, userInfo);
            }
        }

        public void LogCampaignList(string username, List<Campaign> CampaignList)
        {
            if (CampaignList == null || CampaignList.Count == 0)
            {
                _logger.LogInformation("[INFO] Campaign list is empty.{UserInfo}",
                    string.IsNullOrEmpty(username) ? "" : $" User: {username}");
                return;
            }

            foreach (var Campaign in CampaignList)
            {
                string userInfo = string.IsNullOrEmpty(username) ? "" : $" | User: {username}";
                string image = string.IsNullOrWhiteSpace(Campaign.ImageUrl) ? "-" : Campaign.ImageUrl;

                // Düzeltilmiş kısım burası. Değer null ise "-", değilse formatlanmış tarihi yazdırır.
                string startDateString = Campaign.StartDate?.ToString("yyyy-MM-dd") ?? "-";
                string endDateString = Campaign.EndDate?.ToString("yyyy-MM-dd") ?? "-";

                _logger.LogInformation("[INFO] Campaign → ID: {Id} | Title: \"{Title}\" | Desc1: \"{Desc1}\" | Desc2: \"{Desc2}\" | Image: {ImageUrl} | Start: {Start} | End: {End}{UserInfo}",
                    Campaign.Id,
                    Campaign.Title ?? "-",
                    Campaign.Desc1 ?? "-",
                    Campaign.Desc2 ?? "-",
                    image,
                    startDateString, 
                    endDateString,   
                    userInfo);
            }
        }

        public void LogEntityRetrieved(string username, string entityName, object data)
        {
            if (data == null)
            {
                _logger.LogWarning("[INFO] {Entity} not found by {User}", entityName, username);
                return;
            }

            var properties = data.GetType().GetProperties();
            var details = string.Join(" | ", properties.Select(p =>
            {
                var value = p.GetValue(data)?.ToString();
                return $"{p.Name}: {value}";
            }));

            _logger.LogInformation("[INFO] {Entity} retrieved by {User} | {Details}", entityName, username, details);
        }

        public void LogError(int errorCode, string detail)
        {
            string message = ErrorMessages.Messages.ContainsKey(errorCode)
                ? ErrorMessages.Messages[errorCode]
                : "Undefined error";

            _logger.LogError($"[ERROR {errorCode}] {message} | Detail: {detail}");
        }
    }
}