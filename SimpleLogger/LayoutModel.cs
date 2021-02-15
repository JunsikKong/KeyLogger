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
        private static Color PUSH_FONT_COLOR = Color.FromArgb(255, 255, 255, 255);
        private static Color PUSH_BACK_COLOR = Color.FromArgb(240, 0, 0, 0);

        private static Color PULL_FONT_COLOR = Colors.Black;
        private static Color PULL_BACK_COLOR = Colors.LightGray;

        public static Color PushFontColor
        {
            get
            {
                return PUSH_FONT_COLOR;
            }
            set
            {
                if(value != null)
                {
                    PUSH_FONT_COLOR = value;
                }
            }

        }

        public static Color PushBackColor
        {
            get
            {
                return PUSH_BACK_COLOR;
            }
            set
            {
                if (value != null)
                {
                    PUSH_BACK_COLOR = value;
                }
            }

        }

        public static Color PullFontColor
        {
            get
            {
                return PULL_FONT_COLOR;
            }
            set
            {
                if (value != null)
                {
                    PULL_FONT_COLOR = value;
                }
            }

        }

        public static Color PullBackColor
        {
            get
            {
                return PULL_BACK_COLOR;
            }
            set
            {
                if (value != null)
                {
                    PULL_BACK_COLOR = value;
                }
            }

        }
    }
}
