using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models 
{
    [Table("Messages")]
    public class Messages
    {
        [Key]
        [Column("message_id")] 
        public int message_id { get; set; }
        
        [Column("chat_id")]
        public int chat_id { get; set; }
        
        [MaxLength(1000)]
        [Column("message")]
        public string message { get; set; }
        
        [Column("created_at")]
        public DateTime? created_at { get; set; }
        
        [Column("updated_at")]
        public DateTime? updated_at { get; set; }
        
        [ForeignKey("chat_id")]
        public Chats? Chats { get; set; }
    }
}