using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("Lessons")]
    public class Lessons
    {
        [Key] [Column("lesson_id")] public int lesson_id { get; set; }

        [Column("course_id")] public int? course_id { get; set; }
        
        [Column("title")] public string? title { get; set; }

        [Column("description")] public string? description { get; set; }
        
        [Column("image_lesson")] public byte[] image_lesson { get; set; }
        
        [Column("video_lesson")] public byte[] video_lesson { get; set; }
        
        [Column("text_lesson")] public string text_lesson { get; set; }
        
        [Column("created_at")] public DateTime? created_at { get; set; }

        [Column("updated_at")] public DateTime? updated_at { get; set; }

        [ForeignKey("course_id")] public Courses? Course { get; set; }
    }
}