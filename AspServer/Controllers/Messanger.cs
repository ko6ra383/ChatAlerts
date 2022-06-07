using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;
using AspServer.Data;
using AspServer.Models;

namespace AspServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Messanger : ControllerBase
    {
        private readonly AppDbContext db;
        public Messanger(AppDbContext db)
        {
            this.db = db;
        }
        static List<Message> ListOfMessages = new List<Message>();
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string output = "Not found";
            if ((id < ListOfMessages.Count) && (id >= 0))
            {
                output = JsonConvert.SerializeObject(ListOfMessages[id]);
            }
            Console.WriteLine($"Зпрошено сообщение N{id} : {output}");
            return output;
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] Message msg)
        {
            if (msg == null)
            {
                return BadRequest();
            }
            ListOfMessages.Add(msg);
            Console.WriteLine($"Всего сообщений: {ListOfMessages.Count} Посланное сообщение {msg}");
            return new OkResult();
        }

        [HttpPost]
        public int CheckUser([FromBody] User user)
        {
            IEnumerable<User> usersList = db.users;
            var res = usersList.Where(usr => usr.Login == user.Login && usr.Password == user.Password).ToArray();
            if (res.Count() != 1) return -1;
            else return res[0].ID;
        }
    }
}
