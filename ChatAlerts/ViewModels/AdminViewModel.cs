using ChatAlerts.Infrastructure.Commands;
using ChatAlerts.Models;
using ChatAlerts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatAlerts.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private readonly static MessangerClientAPI API = new MessangerClientAPI();
        #region Свойства
        #region BadUser
        private bool _BadUser = false;
        public bool BadUser
        {
            get => _BadUser;
            set => Set(ref _BadUser, value);
        }
        #endregion
        #region BadChat
        private bool _BadChat = false;
        public bool BadChat
        {
            get => _BadChat;
            set => Set(ref _BadChat, value);
        }
        #endregion
        #region BadAddInChat
        private bool _BadAddInChat = false;
        public bool BadAddInChat
        {
            get => _BadAddInChat;
            set => Set(ref _BadAddInChat, value);
        }
        #endregion
        #endregion
        #region Команды 
        #region NewUser
        public ICommand NewUser { get; }
        private bool CanNewUserExecute(object p) => true;
        private void OnNewUserExecute(object p)
        {
            if (!(p is Tuple<string, string> LogPas)) return;
            User user = new()
            {
                Login = LogPas.Item1,
                Password = LogPas.Item2,
                IsAdmin = false
            };
            if (API.AddUser(user) == -1) BadUser = true;
            else BadUser = false;
            OnPropertyChanged("BadUser");

        }
        #endregion
        #region NewChat
        public ICommand NewChat { get; }
        private bool CanNewChatExecute(object p) => true;
        private void OnNewChatExecute(object p)
        {
            if (!(p is string chatName)) return;
            Chat chat = new Chat
            {
                Name = chatName
            };
            if (API.AddChat(chat) == -1) BadChat = true;
            else BadChat = false;
            OnPropertyChanged("BadChat");
        }
        #endregion
        #region AddInChat
        public ICommand AddInChat { get; }
        private bool CanAddInChatExecute(object p) => true;
        private void OnAddInChatExecute(object p)
        {
            if (!(p is Tuple<string, string> UserChat)) return;
            var usrName = UserChat.Item1;
            var chtName = UserChat.Item2;
            int usrID = API.GetUserID(usrName);
            int chtID = API.GetChatID(chtName);
            if(usrID == -1 || chtID == -1) return;
            ChatUser chatUser = new()
            {
                ChatID = chtID,
                UserID = usrID
            };
            if (API.AddChatUser(chatUser) == -1) BadAddInChat = true;
            else BadAddInChat = false;
            OnPropertyChanged("BadAddInChat");
        }
        #endregion
        #endregion 
        public AdminViewModel()
        {
            #region Команды
            NewUser = new LambdaCommand(OnNewUserExecute, CanNewUserExecute);
            NewChat = new LambdaCommand(OnNewChatExecute, CanNewChatExecute);
            AddInChat = new LambdaCommand(OnAddInChatExecute, CanAddInChatExecute);
            #endregion

        }
    }
}
