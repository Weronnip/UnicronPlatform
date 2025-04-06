using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    [Table("UserProgress")]
    public class UserProgress
    {
        [Key]
        [Column("progress_id")]
        public int progress_id { get; set; }

        [Column("enrollments_id")]
        public int? enrollments_id { get; set; }

        [Column("total_lessons_completed")]
        public byte? total_lessons_completed { get; set; }

        [Column("progress_percentage")]
        public decimal? progress_percentage { get; set; }
        
        [ForeignKey("enrollments_id")]
        public Enrollments? Enrollments { get; set; }
    }
}