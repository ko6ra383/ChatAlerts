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
        private static int MessageID;
        private static MessangerClientAPI API = new MessangerClientAPI();
        private ObservableCollection<Message> _messangeList;
        public ObservableCollection<Message> messangeList
        {
            get => _messangeList;
            set => Set(ref _messangeList, value);
        }

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
            set => Set(ref _SelectedChat, value);
        }
        #endregion

        #endregion
        #region testlist
        public List<string> testlist { get; set; } = Enumerable.Range(1, 30).Select(i => $"тестовая строка N{i}").ToList();
        #endregion
        #region Команды
        public ICommand SendMessageCommand { get; }
        private bool CanSendMessageCommandExecute(object p) => true;
        private void OnSendMessageCommandExecute(object p)
        {
            string UserName = "test";
            if (!(p is string msgText)) return;
            string Message = msgText;
            if (UserName.Length > 1 && Message.Length > 1)
            {
                Message msg = new Message(UserName, Message, DateTime.Now);
                API.SendMessage(msg);
            }
        }
        #endregion
        private void Timer_Tick(object sender, EventArgs e)
        {
            var asnk = new Func<Task>(async () =>
            {
                Message msg = await API.GetMessageAsync(MessageID);
                while (msg != null)
                {
                    messangeList.Add(msg);
                    OnPropertyChanged("messangeList");
                    MessageID++;
                    msg = API.GetMessage(MessageID);
                }
            });
            asnk.Invoke();
        }
        public MainViewModel()
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
                //if (mainUser.ID != -1)
                //    MessageBox.Show("Авторизация пройдена");
                //else
                //    MessageBox.Show("Неверный пароль");
            }
            //else
            //{
            //    MessageBox.Show("Авторизация не пройдена");
            //}
            Chats = API.GetChats(mainUser.ID);
            var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += Timer_Tick;
            timer.Start();
            messangeList = new ObservableCollection<Message>();
            #region Команды
            SendMessageCommand = new LambdaCommand(OnSendMessageCommandExecute, CanSendMessageCommandExecute);
            #endregion
        }

        
    }
}
