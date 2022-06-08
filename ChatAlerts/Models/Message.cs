using System;
using System.ComponentModel.DataAnnotations;

namespace ChatAlerts.Models
{
    [Serializable]
    public class Message
    {
        [Key]
        public int? ID { get; set; } = null;
        public int UserID { get; set; }
        public int ChatID { get; set; }
        public string MessageText { get; set; }
        public DateTime TimeStamp { get; set; }
        public Message(int userID, int chatID, string messageText, DateTime timeStamp, int? id = null)
        {
            UserID = userID;
            ChatID = chatID;
            MessageText = messageText;
            TimeStamp = timeStamp;
            ID = id;
        }
    }
}
