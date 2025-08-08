namespace İspark.Model
{
    public class NewsListDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Desc1 { get; set; }
        //public string? ShortDesc => Title?.Length > 50 ? Title[..50] + "..." : Title;
    }
}
