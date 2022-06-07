using System.ComponentModel.DataAnnotations;

namespace AspServer.Models
{
    public class MessangeStatus
    {
        [Key]
        public int Id { get; set; }
        public int MessangeID { get; set; }
        public int UserID { get; set; }
        public bool IsRead { get; set; }
    }
}
