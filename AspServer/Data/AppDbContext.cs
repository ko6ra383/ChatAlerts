using AspServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AspServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> users { get; set; }
        public DbSet<Chat> chats { get; set; }
        public DbSet<ChatUser> chatUsers { get; set; }
        
    }
}
