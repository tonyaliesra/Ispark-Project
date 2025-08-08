using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace İspark.Model
{
    [Table("E_Users")]
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public DateTime? CreateDate { get; set; } 
        public DateTime? UpdateDate { get; set; } 
    }
}