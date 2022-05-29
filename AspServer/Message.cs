using System;

namespace AspServer
{
    [Serializable]
    public class Message
    {

        public string UserName { get; set; }
        public string MessageText { get; set; }
        public DateTime TimeStamp { get; set; }
        public Message(string userName, string messageText, DateTime timeStamp)
        {
            UserName = userName;
            MessageText = messageText;
            TimeStamp = timeStamp;
        }
        public Message()
        {
            UserName = "System";
            MessageText = "Server is running...";
            TimeStamp = DateTime.Now;
        }
        public override string ToString()
        {
            return $"{UserName} <{TimeStamp}>: {MessageText}";
        }
    }
}
