using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("UserCourse")]
    public class UserCourse
    {
        [Column("user_id")]
        public int user_id { get; set; }
        
        [Column("course_id")]
        public int course_id { get; set; }
        
        [Column("pay_id")]
        public int pay_id { get; set; }
        
        [ForeignKey("user_id")]
        public Users? Users { get; set; }
        
        [ForeignKey("course_id")]
        public Courses? Courses { get; set; }
        
        [ForeignKey("pay_id")]
        public Payments? Payments { get; set; }
    }
}