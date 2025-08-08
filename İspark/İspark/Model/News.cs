// Konum: İspark.Model/News.cs

using System.ComponentModel.DataAnnotations.Schema;

namespace İspark.Model
{
    [Table("E_News")]
    public class News
    {
        public int Id { get; set; }
        public string? Desc1 { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Desc2 { get; set; }
        public DateTime? CreateDate { get; set; } // Nullable yapıldı
        public DateTime? UpdateDate { get; set; } // Nullable yapıldı
    }
}