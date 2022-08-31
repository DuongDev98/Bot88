using BOT_IT_V1.Utils;
using SocketIOClient;
using System;
using System.Drawing;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace BOT_IT_V1
{
    /// <summary>
    /// Interaction logic for Ping.xaml
    /// </summary>
    public partial class Ping : Window
    {
        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
        Timer tmrXanhDo;
        private Dispatcher _dispathcer;
        int counter = 0, maxCounter = 50;
        TypePing typePing;
        string code = "";
        public Ping(string _code)
        {
            InitializeComponent();

            code = _code;

            _dispathcer = Dispatcher.CurrentDispatcher;
            typePing = TypePing.XanhDo;

            nIcon.Icon = new Icon(@"icon.ico");
            nIcon.Click += NIcon_Click;

            setBtnXanh(true);
            setBtnDo(true);

            counter = 0;
            tmrXanhDo = new Timer();
            tmrXanhDo.Interval = 100;
            tmrXanhDo.Elapsed += TmrXanhDo_Elapsed;
            tmrXanhDo.Enabled = true;
        }

        private void TmrXanhDo_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmrXanhDo.Enabled = false;
            counter++;
            if (counter > maxCounter)
            {
                counter = 0;
                _dispathcer.Invoke(new Action(() => {
                    setBtnXanh(true);
                    setBtnDo(true);
                }));
            }
            else
            {
                if (typePing == TypePing.XanhDo)
                {
                    _dispathcer.Invoke(new Action(() => {
                        setBtnXanh(counter % 2 == 0);
                        setBtnDo(counter % 2 != 0);
                    }));
                }
                else if (typePing == TypePing.Xanh)
                {
                    _dispathcer.Invoke(new Action(() => {
                        setBtnXanh(counter % 2 == 0);
                    }));
                }
                else if (typePing == TypePing.Do)
                {
                    _dispathcer.Invoke(new Action(() => {
                        setBtnDo(counter % 2 == 0);
                    }));
                }
                tmrXanhDo.Enabled = true;
            }
        }

        private void setBtnDo(bool v)
        {
            btnDo.Background = v ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
        }

        private void setBtnXanh(bool v)
        {
            btnXanh.Background = v ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
        }

        private void NIcon_Click(object sender, System.EventArgs e)
        {
            nIcon.Visible = false;
            this.Show();
        }

        private void btnAn_Click(object sender, RoutedEventArgs e)
        {
            nIcon.Visible = true;
            this.Hide();
        }

        SocketIO ws;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ws = new SocketIO("http://api-bot88.winddevteam.tk");

            ws.On("notification", response =>
            {
                string text = response.GetValue<string>().Trim();
                if (text == "xanh")
                {
                    typePing = TypePing.Xanh;
                    tmrXanhDo.Enabled = true;
                }
                else
                {
                    typePing = TypePing.Do;
                    tmrXanhDo.Enabled = true;
                }
            });

            ws.OnConnected += Ws_OnConnected;

            ws.OnError += Ws_OnError;

            ws.ConnectAsync();
        }

        private void Ws_OnError(object sender, string e)
        {
            Msg.ShowWarning("Có lỗi trong quá trình kết nốt cổng dữ liệu: " + Environment.NewLine + e);
        }

        private async void Ws_OnConnected(object sender, EventArgs e)
        {
            await ws.EmitAsync("joinRoom", code);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }
    }

    public enum TypePing
    {
        XanhDo = 0,
        Xanh = 1,
        Do = 2
    }
}
