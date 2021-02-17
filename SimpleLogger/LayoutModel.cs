using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleLogger
{
    public static class LayoutModel
    {
        static LayoutModel()
        {
            PushFontColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            PushBackColor = new SolidColorBrush(Color.FromArgb(240, 0, 0, 0)); 
            PullFontColor = new SolidColorBrush(Colors.Black);
            PullBackColor = new SolidColorBrush(Colors.LightGray);
        }

        public static SolidColorBrush PushFontColor { get; set; }

        public static SolidColorBrush PushBackColor { get; set; }

        public static SolidColorBrush PullFontColor { get; set; }

        public static SolidColorBrush PullBackColor { get; set; }
    }
}
