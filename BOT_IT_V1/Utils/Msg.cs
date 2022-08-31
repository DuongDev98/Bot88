using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BOT_IT_V1.Utils
{
    internal class Msg
    {
        public static void ShowInfo(string text)
        {
            MessageBox.Show(text, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static void ShowWarning(string text)
        {
            MessageBox.Show(text, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
