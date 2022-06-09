using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;
using AspServer.Data;
using AspServer.Models;

namespace AspServer.Controllers
{
    [ApiController]
    public class Messanger : ControllerBase
    {
        private readonly AppDbContext db;
        public Messanger(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("ChatMessenges/{chatID}")]
        public IEnumerable<Message> Get(int chatID)
        {
            return db.Messages.Where(x => x.ChatID == chatID);
        }

        [HttpPost]
        [Route("Send")]
        public IActionResult SendMessage([FromBody] Message msg)
        {
            if (msg == null)
            {
                return BadRequest();
            }
            db.Messages.Add(msg);
            db.SaveChanges();
            Console.WriteLine($"Всего сообщений: {db.Messages.Count()} Посланное сообщение: {msg.MessageText}");
            return new OkResult();
        }

        [HttpPost]
        [Route("Check")]
        public int CheckUser([FromBody] User user)
        {
            IEnumerable<User> usersList = db.users;
            var res = usersList.Where(usr => usr.Login == user.Login && usr.Password == user.Password).ToArray();
            if (res.Count() != 1) return -1;
            else return res[0].ID;
        }
        [HttpGet]
        [Route("Chats/{id}")]
        public IEnumerable<Chat> GetChats(int id)
        {
            var chatsID = db.chatUsers.Where(x => x.UserID == id);
            var chats = db.chats.Where(c => chatsID.Any(x => x.ChatID == c.Id));
            return chats;
        }
        [HttpGet]
        [Route("UsersInGroup/{chatID}")]
        public IEnumerable<User> GetUsers(int chatID)
        {
            var chatsID = db.chatUsers.Where(x => x.ChatID == chatID);
            var users = db.users.Where(c => chatsID.Any(x => x.UserID == c.ID));
            return users;
        }
    }
}
