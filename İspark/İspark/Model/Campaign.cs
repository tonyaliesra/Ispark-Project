using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace İspark.Model
{
    [Table("E_Campaign")]
    public class Campaign
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Desc1 { get; set; }
        public string? Desc2 { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }  
        public DateTime? CreateDate { get; set; } 
        public DateTime? UpdateDate { get; set; } 
        public bool? IsDeleted { get; set; }      
    }
}