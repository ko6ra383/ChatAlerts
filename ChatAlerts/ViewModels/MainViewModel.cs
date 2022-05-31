using ChatAlerts.Infrastructure.Commands;
using ChatAlerts.Models;
using ChatAlerts.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
