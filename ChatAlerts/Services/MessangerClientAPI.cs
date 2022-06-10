using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChatAlerts.Models;
using Newtonsoft.Json;
using ChatAlerts.Models;
using System.Collections.Generic;

namespace ChatAlerts.Services
{
    public class MessangerClientAPI
    {
        //public void TestJson()
        //{
        //    Message msg = new Message("Sas", "privet", DateTime.UtcNow);
        //    string output = JsonConvert.SerializeObject(msg);
        //    Console.WriteLine(output);
        //    Message des = JsonConvert.DeserializeObject<Message>(output);
        //    Console.WriteLine(des);
        //}
        public IEnumerable<Message> GetMessage(int? chatID)
        {
            if (chatID == null) return null;
            WebRequest request = WebRequest.Create("http://localhost:5000/ChatMessenges/" + chatID.ToString());
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            string status = ((HttpWebResponse)response).StatusDescription;
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            if (status.ToLower() == "ok" && responseFromServer != "Not found")
            {
                IEnumerable<Message> deserializatedMsgs = JsonConvert.DeserializeObject<IEnumerable<Message>>(responseFromServer);
                return deserializatedMsgs;
            }
            return null;
        }
        public int GetChatID(string chatName)
        {
            if (chatName == null) return -1;
            WebRequest request = WebRequest.Create("http://localhost:5000/GetChatID/" + chatName.ToString());
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            string status = ((HttpWebResponse)response).StatusDescription;
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            if (status.ToLower() == "ok" && responseFromServer != "Not found")
            {
                return int.Parse(responseFromServer);
            }
            return -1;
        }
        public int GetUserID(string userName)
        {
            if (userName == null) return -1;
            WebRequest request = WebRequest.Create("http://localhost:5000/GetUserID/" + userName.ToString());
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            string status = ((HttpWebResponse)response).StatusDescription;
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            if (status.ToLower() == "ok" && responseFromServer != "Not found")
            {
                return int.Parse(responseFromServer);
            }
            return -1;
        }
        //private static readonly HttpClient client = new HttpClient();
        //public async Task<Message> GetMessageAsync(int MessageID)
        //{
        //    var responseString = await client.GetStringAsync("http://localhost:5000/ChatMessenges/" + MessageID.ToString());
        //    if (responseString != null)
        //    {
        //        Message deserializedMsg = JsonConvert.DeserializeObject<Message>(responseString);
        //        return deserializedMsg;
        //    }
        //    return null;
        //}
        public bool SendMessage(Message msg)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/Send");
            request.Method = "POST";
            string postData = JsonConvert.SerializeObject(msg);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return true;
        }
        public int CheckUser(User user)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/Check");
            request.Method = "POST";
            string postData = JsonConvert.SerializeObject(user);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return int.Parse(responseFromServer);
        }
        public int AddUser(User user)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/AddUser");
            request.Method = "POST";
            string postData = JsonConvert.SerializeObject(user);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return int.Parse(responseFromServer);
        }
        public int AddChat(Chat user)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/AddChat");
            request.Method = "POST";
            string postData = JsonConvert.SerializeObject(user);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return int.Parse(responseFromServer);
        }
        public int AddChatUser(ChatUser chatuser)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/AddChatUser");
            request.Method = "POST";
            string postData = JsonConvert.SerializeObject(chatuser);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return int.Parse(responseFromServer);
        }

        public IEnumerable<Chat> GetChats(int userID)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/Chats/" + userID.ToString());
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            string status = ((HttpWebResponse)response).StatusDescription;
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            if (status.ToLower() == "ok" && responseFromServer != "Not found")
            {
                IEnumerable<Chat> deserializatedChats = JsonConvert.DeserializeObject<IEnumerable<Chat>>(responseFromServer);
                return deserializatedChats;
            }
            return null;
        }
        public IEnumerable<User> GetUsers(int chatID)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/UsersInGroup/" + chatID.ToString());
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            string status = ((HttpWebResponse)response).StatusDescription;
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            if (status.ToLower() == "ok" && responseFromServer != "Not found")
            {
                IEnumerable<User> deserializatedChats = JsonConvert.DeserializeObject<IEnumerable<User>>(responseFromServer);
                return deserializatedChats;
            }
            return null;
        }
    }
}
