using AspServer.Data;
using AspServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AspServer.Controllers
{
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly AppDbContext db;
        public ChatsController(AppDbContext db)
        {
            this.db = db;
        }

        [Route("Chats/{id}")]
        public IEnumerable<Chat> GetChats(int id)
        {
            var chatsID = db.chatUsers.Where(x => x.UserID == id);
            var chats = db.chats.Where(c => chatsID.Any(x => x.ChatID == c.Id));
            return chats;
        }
    }
}
