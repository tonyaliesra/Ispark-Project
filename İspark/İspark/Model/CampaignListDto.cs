namespace İspark.Model
{
    public class CampaignListDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Desc1 { get; set; }
        public string? ImageUrl { get; set; }
        //public string? ShortDesc => Title?.Length > 40 ? Title[..40] + "..." : Title;
    }
}
