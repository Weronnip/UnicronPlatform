using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int category_id { get; set; }

        [Required]
        [Column("name")]
        public string name { get; set; }

        [Column("description")]
        public string? description { get; set; }
        
        public List<Courses> Courses { get; set; } = new();
    }
}