using ChatAlerts.Infrastructure.Commands;
using ChatAlerts.Models;
using ChatAlerts.Services;
using ChatAlerts.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatAlerts.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Свойства
        private readonly static MessangerClientAPI API = new MessangerClientAPI();

        #region User
        private User _mainUser;
        public User mainUser
        {
            get => _mainUser;
            set => Set(ref _mainUser, value);
        }
        #endregion
        #region Chats
        private IEnumerable<Chat> _Chats;
        public IEnumerable<Chat> Chats
        {
            get => _Chats;
            set => Set(ref _Chats, value);
        }
        #endregion
        #region SelectedChat
        private Chat _SelectedChat;
        public Chat SelectedChat
        {
            get => _SelectedChat;
            set
            {
                Set(ref _SelectedChat, value);
                UsersInChat = API.GetUsers(SelectedChat.Id);
            }
        }
        #endregion
        #region Messanges
        private IEnumerable<Message> _Messanges;
        public IEnumerable<Message> Messanges
        {
            get => _Messanges;
            set => Set(ref _Messanges, value);
        }
        #endregion
        #region SelectedMessange
        private Message _SelectedMessange;
        public Message SelectedMessange
        {
            get => _SelectedMessange;
            set => Set(ref _SelectedMessange, value);
        }
        #endregion

        #region UsersInGroup
        private IEnumerable<User> _UsersInChat;
        public IEnumerable<User> UsersInChat
        {
            get => _UsersInChat;
            set => Set(ref _UsersInChat, value);
        }
        #endregion

        #endregion
        #region testlist
        public List<string> testlist { get; set; } = Enumerable.Range(1, 30).Select(i => $"тестовая строка N{i}").ToList();
        #endregion
        #region Команды
        #region SendMessageCommand
        public ICommand SendMessageCommand { get; }
        private bool CanSendMessageCommandExecute(object p) => true;
        private void OnSendMessageCommandExecute(object p)
        {
            if (!(p is string msgText)) return;
            string Message = msgText;
            if (Message.Length > 1)
            {
                Message msg = new Message
                {
                    MessageText = Message,
                    ChatID = SelectedChat.Id,
                    UserID = mainUser.ID,
                    TimeStamp = DateTime.Now
                };
                API.SendMessage(msg);
            }
        }
        #endregion
        #region AdminPanelCommand
        public ICommand AdminPanelCommand { get; }
        private bool CanAdminPanelCommandExecute(object p) => true;
        private void OnAdminPanelCommandExecute(object p)
        {
            new AdminPanelWin().Show();
        }
        #endregion
        #endregion
        private void Timer_Tick(object sender, EventArgs e)
        {
            //var asnk = new Func<Task>(async () =>
            //{
            //    Message msg = await API.GetMessageAsync(MessageID);
            //    while (msg != null)
            //    {
            //        messangeList.Add(msg);
            //        OnPropertyChanged("messangeList");
            //        MessageID++;
            //        msg = API.GetMessage(MessageID);
            //    }
            //});
            //asnk.Invoke();
            Messanges = API.GetMessage(SelectedChat?.Id);
        }
        public MainViewModel()
        {
            if (!App.IsDesingnMode)
            {
                EnterWin enterWindow = new EnterWin();
                if (enterWindow.ShowDialog() == true)
                {
                    mainUser = new User()
                    {
                        ID = 228,
                        Login = enterWindow.Login,
                        Password = enterWindow.Password,
                        IsAdmin = enterWindow.Login == "admin" ? true : false
                    };
                    mainUser.ID = API.CheckUser(mainUser);
                    if (mainUser.ID == -1)
                        Application.Current.Shutdown();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
            Chats = API.GetChats(mainUser.ID);
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += Timer_Tick;
            timer.Start();
            #region Команды
            SendMessageCommand = new LambdaCommand(OnSendMessageCommandExecute, CanSendMessageCommandExecute);
            AdminPanelCommand = new LambdaCommand(OnAdminPanelCommandExecute, CanAdminPanelCommandExecute);
            #endregion
        }

        
    }
}
