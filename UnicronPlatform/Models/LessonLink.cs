using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("LessonLink")]
    public class LessonLink
    {
        [Key]
        [Column("link_id")]
        public int link_id { get; set; }

        [Column("link_name")]
        public string? link_name { get; set; }

        [Column("path_link")]
        public string? path_link { get; set; }
        
        public List<Lessons>? Lessons { get; set; }
    }

}