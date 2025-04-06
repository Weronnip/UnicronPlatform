using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("Enrollments")]
    public class Enrollments
    {
        [Key]
        [Column("enrollment_id")]
        public int enrollment_id { get; set; }

        [Column("user_id")]
        public int? user_id { get; set; }

        [Column("course_id")]
        public int? course_id { get; set; }

        [Column("status")]
        public bool? status { get; set; }

        [Column("created_at")]
        public DateTime? created_at { get; set; }

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }
        
        [ForeignKey("user_id")]
        public Users? User { get; set; }
        
        [ForeignKey("course_id")]
        public Courses? Course { get; set; }
        
        public UserProgress? UserProgress { get; set; }
    }
}