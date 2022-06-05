using System.ComponentModel.DataAnnotations;

namespace AspServer.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
