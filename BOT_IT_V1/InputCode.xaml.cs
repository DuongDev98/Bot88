using BOT_IT_V1.Utils;
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
using System.Windows.Shapes;

namespace BOT_IT_V1
{
    /// <summary>
    /// Interaction logic for InputCode.xaml
    /// </summary>
    public partial class InputCode : Window
    {
        public bool ok = false;
        public InputCode()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            KiemTraCode();
        }

        private void KiemTraCode()
        {
            //Kiểm tra dữ liệu nhập
            string code = txtCode.Text.Trim();
            if (code.Length == 0)
            {
                Msg.ShowWarning("Mã ứng dụng không được trống!");
            }
            else
            {
                BaseResult rs = HttpUtils.PostRequest("code/verify/" + code);
                if (rs.success)
                {
                    ok = true;
                    Close();
                }
                else
                {
                    Msg.ShowWarning(rs.message);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                KiemTraCode();
            }
        }
    }
}
