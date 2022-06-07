using System.ComponentModel.DataAnnotations;

namespace AspServer.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }


}
