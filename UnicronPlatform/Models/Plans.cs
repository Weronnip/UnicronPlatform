using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("Plans")]
    public class Plans
    {
        [Key]
        [Column("plan_id")]
        public int plan_id { get; set; }

        [Required]
        [Column("name")]
        public string name { get; set; }

        [Column("price")]
        public decimal? price { get; set; }

        [Column("duration")]
        public int? duration { get; set; }

        [Column("description")]
        public string? description { get; set; }
    }

}