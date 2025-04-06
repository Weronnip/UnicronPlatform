using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("Subscriptions")]
    public class Subscriptions
    {
        [Key]
        [Column("subscription_id")]
        public int subscription_id { get; set; }

        [Column("user_id")]
        public int? user_id { get; set; }

        [Column("plan_id")]
        public int? plan_id { get; set; }

        [Column("start_date")]
        public DateTime? start_date { get; set; }

        [Column("end_date")]
        public DateTime? end_date { get; set; }

        [Column("status")]
        public byte? status { get; set; }
        
        [ForeignKey("user_id")]
        public Users? User { get; set; }
        
        [ForeignKey("plan_id")]
        public Plans? Plan { get; set; }
    }

}