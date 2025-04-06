using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [Column("role_id")]
        public int role_id { get; set; }

        [Required]
        [Column("name_role")]
        public string name_role { get; set; }

        [Column("description_role")]
        public string? description_role { get; set; }

        public List<Users> Users { get; set; } = new();
        public List<Instructor> Instructor { get; set; } = new();
    }
}