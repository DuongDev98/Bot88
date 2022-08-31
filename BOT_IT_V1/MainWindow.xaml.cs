using BOT_IT_V1.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BOT_IT_V1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DownloadData();
        }

        private void DownloadData()
        {
            pnDetail.Children.Clear();
            BaseResult rs = HttpUtils.GetRequest("game/list-icon");
            if (rs.success)
            {
                JArray gameArr = JArray.Parse(rs.data);
                foreach (JValue item in gameArr)
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(item.ToString(), UriKind.Absolute));
                    img.Stretch = Stretch.UniformToFill;
                    img.HorizontalAlignment = HorizontalAlignment.Center;
                    img.VerticalAlignment = VerticalAlignment.Center;

                    Button btn = new Button();
                    btn.Content = img;
                    btn.Height = 100;
                    btn.Width = 100;
                    btn.Margin = new Thickness(5);
                    btn.Cursor = Cursors.Hand;
                    btn.Click += Btn_Click;
                    pnDetail.Children.Add(btn);
                }
            }
            else
            {
                Msg.ShowWarning("Không thể tải danh sách game:" + Environment.NewLine + rs.message);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            InputCode fInputCode = new InputCode();
            fInputCode.ShowDialog();
            if (fInputCode.ok)
            {
                this.Hide();
                LoaddingUi fLoadding = new LoaddingUi();
                fLoadding.ShowDialog();

                Ping fPing = new Ping(fInputCode.txtCode.Text.Trim());
                fPing.ShowDialog();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
