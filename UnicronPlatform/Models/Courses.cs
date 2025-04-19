using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("Courses")]
    public class Courses
    {
        [Key]
        [Column("course_id")]
        public int course_id { get; set; }

        [Column("category_id")]
        public int? category_id { get; set; }

        [Column("instructor_id")]
        public int? instructor_id { get; set; }

        [Required]
        [Column("title")]
        public string title { get; set; }

        [Required]
        [Column("description")]
        public string description { get; set; }

        [Column("price")]
        public decimal? price { get; set; }

        [Column("total_lessons")]
        public int? total_lessons { get; set; }

        [Column("control_point")]
        public int? control_point { get; set; }

        [Column("created_at")]
        public DateTime? created_at { get; set; }

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }
        
        [ForeignKey("category_id")]
        public Category? Category { get; set; }
        
        [ForeignKey("instructor_id")]
        public Instructor? Instructor { get; set; }
        
        public List<Lessons>? Lessons { get; set; }
        public List<Enrollments>? Enrollments { get; set; }

    }

}