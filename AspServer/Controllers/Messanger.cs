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
            var res = db.users.Where(usr => usr.Login == user.Login && usr.Password == user.Password).ToArray();
            if (res.Length != 1) return -1;
            else return res[0].ID;
        }
        [HttpGet]
        [Route("GetChatID/{name}")]
        public int GetChatID(string name)
        {
            var res = db.chats.Where(usr => usr.Name == name).ToArray();
            if (res.Length != 1) return -1;
            else return res[0].Id;
        }
        [HttpGet]
        [Route("GetUserID/{name}")]
        public int GetUserID(string name)
        {
            var res = db.users.Where(usr => usr.Login == name).ToArray();
            if (res.Length != 1) return -1;
            else return res[0].ID;
        }
        [HttpPost]
        [Route("AddUser")]
        public int AddUser([FromBody] User user)
        {
            var res = db.users.Where(usr => usr.Login == user.Login).ToArray();
            if (res.Length > 0) return -1;
            db.users.Add(user);
            db.SaveChanges();
            return 1;
        }
        [Route("AddChat")]
        public int AddChat([FromBody] Chat chat)
        {
            var res = db.chats.Where(cu => cu.Name == chat.Name).ToArray();
            if (res.Length > 0) return -1;
            db.chats.Add(chat);
            db.SaveChanges();
            return 1;
        }
        [Route("AddChatUser")]
        public int AddChatUser([FromBody] ChatUser chatUser)
        {
            var res = db.chatUsers.Where(cu => cu.ChatID == chatUser.ChatID && cu.UserID == chatUser.UserID).ToArray();
            if (res.Length > 0) return -1;
            db.chatUsers.Add(chatUser);
            db.SaveChanges();
            return 1;
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
