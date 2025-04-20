using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models 
{
    [Table("Payments")]
    public class Payments
    {
        [Key]
        [Column("pay_id")]
        public int pay_id { get; set; }
        
        [Column("user_id")]
        public int user_id { get; set; }
        [Column("course_id")]
        public int? course_id { get; set; }
        
        [Column("plan_id")]
        public int? plan_id { get; set; }
        [Column("amount")]
        public decimal amount { get; set; }
        
        [Column("service_fee")]
        public decimal service_fee { get; set; }
        
        [Column("tax")]
        public decimal tax { get; set; }
        
        [Column("author_share")]
        public decimal author_share { get; set; }
        
        [Column("is_plan")]
        public bool is_plan { get; set; }
        
        [Column("created_at")]
        public DateTime created_at { get; set; }
        
        [ForeignKey("course_id")]
        public Courses? Courses { get; set; }
        
        [ForeignKey("user_id")]
        public Users? Users { get; set; }
        
        [ForeignKey("plan_id")]
        public Plans? Plans { get; set; }
    }
}
