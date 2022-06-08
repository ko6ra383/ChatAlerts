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
        //public Message GetMessage(int MessageID)
        //{
        //    WebRequest request = WebRequest.Create("http://localhost:5000/api/Messanger/Get" + MessageID.ToString());
        //    request.Method = "GET";
        //    WebResponse response = request.GetResponse();
        //    string status = ((HttpWebResponse)response).StatusDescription;
        //    Stream dataStream = response.GetResponseStream();
        //    StreamReader reader = new StreamReader(dataStream);
        //    string responseFromServer = reader.ReadToEnd();
        //    reader.Close();
        //    dataStream.Close();
        //    response.Close();
        //    if (status.ToLower() == "ok" && responseFromServer != "Not found")
        //    {
        //        Message deserializatedMsg = JsonConvert.DeserializeObject<Message>(responseFromServer);
        //        return deserializatedMsg;
        //    }
        //    return null;
        //}
        //private static readonly HttpClient client = new HttpClient();
        //public async Task<Message> GetMessageAsync(int MessageID)
        //{
        //    var responseString = await client.GetStringAsync("http://localhost:5000/api/Messanger/Get" + MessageID.ToString());
        //    if (responseString != null)
        //    {
        //        Message deserializedMsg = JsonConvert.DeserializeObject<Message>(responseString);
        //        return deserializedMsg;
        //    }
        //    return null;
        //}
        //public bool SendMessage(Message msg)
        //{
        //    WebRequest request = WebRequest.Create("http://localhost:5000/api/Messanger/SendMessage");
        //    request.Method = "POST";
        //    string postData = JsonConvert.SerializeObject(msg);
        //    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //    request.ContentType = "application/json";
        //    request.ContentLength = byteArray.Length;
        //    Stream dataStream = request.GetRequestStream();
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    dataStream.Close();
        //    WebResponse response = request.GetResponse();
        //    dataStream = response.GetResponseStream();
        //    StreamReader reader = new StreamReader(dataStream);
        //    string responseFromServer = reader.ReadToEnd();
        //    reader.Close();
        //    dataStream.Close();
        //    response.Close();
        //    return true;
        //}
        public int CheckUser(User user)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/api/Messanger/CheckUser");
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

        public IEnumerable<Chat> GetChats(int id)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/Chats/" + id.ToString());
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
    }
}
