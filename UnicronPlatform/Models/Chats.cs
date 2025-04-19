using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnicronPlatform.Models
{
    
    [Table("Chats")]
    public class Chats
    {
        [Key]
        [Column("chat_id")] 
        public int chat_id { get; set; }
        
        [Column("user_id")]
        public int user_id { get; set; }
        
        [Column("support_id")]
        public int support_id { get; set; }
        
        [ForeignKey("support_id")]
        public Supports? Supports { get; set; }
        
        [ForeignKey("user_id")]
        public Users? Users { get; set; }
    }
}