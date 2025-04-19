using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("Supports")]
    public class Supports
    {
        [Key]
        [Column("support_id")]
        public int support_id { get; set; }
            
        [Column("role_id")]
        public int role_id { get; set; }
            
        [ForeignKey("role_id")]
        public Role? Role { get; set; }
    }
}