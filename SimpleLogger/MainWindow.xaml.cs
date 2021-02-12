using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

namespace SimpleLogger
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow
    {
        const int H_MASK = 1000 * 60 * 60;
        const int M_MASK = 1000 * 60;
        const int S_MASK = 1000;

        Thread RunThread = null;

        [DllImport("user32.dll")]
        public static extern UInt16 GetAsyncKeyState(Int32 vKey);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

            if (RunThread==null)
            {
                RunThread = new Thread(() => RunAct());
                RunThread.IsBackground = true;
            }

            if (!RunThread.IsAlive)
            {
                string nowTime = DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss");
                tbxLog.AppendText($"[{nowTime}] Thread 실행\n");
                tbxLog.ScrollToEnd();

                RunThread.Start();
            }
            else
            {
                tbxLog.AppendText("이미 실행중 입니다.\n");
                tbxLog.ScrollToEnd();
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (RunThread.IsAlive)
            {
                RunThread.Interrupt();
                RunThread = null;

                string nowTime = DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss");
                tbxLog.AppendText($"[{nowTime}] Thread 종료\n");
                tbxLog.ScrollToEnd();
            }
            else
            {
                tbxLog.AppendText("실행중이 아닙니다.\n");
                tbxLog.ScrollToEnd();
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {

        }

        void RunAct()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            bool[] isPrevPush = new bool[0xFE];

            for (int i = 0; i < 0xFE; i++) isPrevPush[i] = false;

            try
            {
                while (true)
                {
                    for (int i = 0; i < 0xFE; i++)
                    {
                        int current = GetAsyncKeyState(i);

                        bool isPush = (current > 1) ? true : false;


                        if (isPush != isPrevPush[i])
                        {
                            this.Invoke(new Action(() => InvokeUIAction(i, (int)sw.ElapsedMilliseconds, isPush)));
                            isPrevPush[i] = isPush;
                        }
                    }
                    Thread.Sleep(1);
                }
            }
            catch(ThreadInterruptedException e)
            {

            }
            finally
            {

            }
        }

        void InvokeUIAction(int keyCode, int time, bool isPsuh)
        {
            int h = time / H_MASK;
            time -= h * H_MASK;
            int m = time / M_MASK;
            time -= m * M_MASK;
            int s = time / S_MASK;
            time -= s * S_MASK;

            if (keyCode >= 33 && keyCode <= 126)
            {
                char c = (char)keyCode;
                string str = c + "키";
                tbxLog.AppendText($"[{h.ToString("00")}:{m.ToString("00")}:{s.ToString("00")}:{time.ToString("000")}] {str} , {isPsuh}\n");
            }
            else
            {
                tbxLog.AppendText($"[{h.ToString("00")}:{m.ToString("00")}:{s.ToString("00")}:{time.ToString("000")}] {keyCode} , {isPsuh}\n");
            }

            tbxLog.ScrollToEnd(); // 스크롤 아래로
        }
    }
}
