using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BOT_IT_V1
{
    /// <summary>
    /// Interaction logic for LoaddingUi.xaml
    /// </summary>
    public partial class LoaddingUi : Window
    {
        Timer tmrDemNguoc, tmrEnd;
        private Dispatcher _dispathcer;
        public LoaddingUi()
        {
            InitializeComponent();

            _dispathcer = Dispatcher.CurrentDispatcher;

            processBar.Minimum = 0;
            processBar.Maximum = 100;
            processBar.Value = 0;

            lblThongBao.Content = "Loadding: 0%";

            tmrDemNguoc = new Timer();
            tmrDemNguoc.Interval = 50;
            tmrDemNguoc.Elapsed += TmrDemNguoc_Elapsed;
            tmrDemNguoc.Enabled = true;

            tmrEnd = new Timer();
            tmrEnd.Interval = 3000;
            tmrEnd.Elapsed += TmrEnd_Elapsed;
        }

        private void TmrEnd_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmrEnd.Enabled = false;
            _dispathcer.Invoke(new Action(() => {
                this.Close();
            }));
        }

        private void TmrDemNguoc_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmrDemNguoc.Enabled = false;
            _dispathcer.Invoke(new Action(() => {
                if (processBar.Value <= 99)
                {
                    processBar.Value = processBar.Value + 1;
                    lblThongBao.Content = "Loadding: " + processBar.Value + "%";
                    tmrDemNguoc.Enabled = true;
                }
                else
                {
                    lblTitle.Content = "LẤY DỮ LIỆU CỔNG GAME THÀNH CÔNG!";
                    tmrEnd.Enabled = true;
                }
            }));
        }
    }
}
