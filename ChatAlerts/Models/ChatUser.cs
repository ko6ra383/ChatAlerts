using System.ComponentModel.DataAnnotations;

namespace ChatAlerts.Models
{
    public class ChatUser
    {
        [Key]
        public int ID { get; set; }
        public int ChatID { get; set; }
        public int UserID { get; set; }
    }
}
