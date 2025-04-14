using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [Column("user_id")]
        public int user_id { get; set; }

        [Required]
        [Column("first_name")]
        public string first_name { get; set; }

        [Required]
        [Column("last_name")]
        public string last_name { get; set; }

        [Required]
        [Column("email")]
        public string email { get; set; }

        [Required]
        [Column("password")]
        public string password { get; set; }

        [Required]
        [Column("phone")]
        public string phone { get; set; }

        [Column("birth_date")]
        public DateTimeOffset? birth_date { get; set; }

        [Column("role_id")]
        public int? role_id { get; set; }

        [Column("avatar")]
        public string? avatar { get; set; }

        [Column("balance")]
        public decimal? balance { get; set; }

        [Column("created_at")]
        public DateTime? created_at { get; set; }

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }
        
        [ForeignKey("role_id")]
        public Role? Role { get; set; }
        
        public List<Enrollments>? Enrollments { get; set; }
        public List<Subscriptions>? Subscriptions { get; set; }

    }
}