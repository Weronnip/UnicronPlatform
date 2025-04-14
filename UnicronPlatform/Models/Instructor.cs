using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("Instructor")]
    public class Instructor
    {
        [Key]
        [Column("instructor_id")]
        public int instructor_id { get; set; }

        [Column("role_id")]
        public int? role_id { get; set; }

        [Required]
        [Column("first_name")]
        public string first_name { get; set; }

        [Required]
        [Column("last_name")]
        public string last_name { get; set; }

        [Column("email")]
        public string? email { get; set; }

        [Column("bio")]
        public string? bio { get; set; }

        [Column("experience")]
        public DateTimeOffset? experience { get; set; }

        [Column("specialization")]
        public string? specialization { get; set; }

        [Column("social_link_wa")]
        public string? social_link_wa { get; set; }

        [Column("social_link_vk")]
        public string? social_link_vk { get; set; }

        [Column("social_link_tg")]
        public string? social_link_tg { get; set; }

        [Column("created_at")]
        public DateTime? created_at { get; set; }

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }
        
        [ForeignKey("role_id")]
        public Role? Role { get; set; }
        
        public List<Courses> Courses { get; set; } = new();
    }
}
    
