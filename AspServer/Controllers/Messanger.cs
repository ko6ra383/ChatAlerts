using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
namespace AspServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Messanger : ControllerBase
    {
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
    }
}
