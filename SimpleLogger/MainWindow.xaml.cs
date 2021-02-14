using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        Thread RunThread = null;
        KeysModel km = new KeysModel();

        private double pauseTime = 0;
        private bool isThreadEnding = false;
        private bool isThreadRun = false;
        private bool isTimeSave = false;


        [DllImport("user32.dll")]
        public static extern UInt16 GetAsyncKeyState(Int32 vKey);
        public MainWindow()
        {
            InitializeComponent();
        }

        void ThreadExit(Thread tr, bool isDelay)
        {
            tr.Interrupt();

            RunThread = null;

            while (isThreadEnding == false && isDelay)
            {
                Thread.Sleep(1);
            }

            isThreadEnding = false;
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
                            this.Invoke(new Action(() => InvokeUIAction(i, sw.ElapsedMilliseconds + pauseTime, isPush)));
                            isPrevPush[i] = isPush;
                        }
                    }
                    Thread.Sleep(1);
                }
            }
            catch(ThreadInterruptedException)
            {
                pauseTime = isTimeSave ? pauseTime + sw.ElapsedMilliseconds : 0;
                isThreadEnding = true;
            }
            finally
            {
                
            }
        }

        void InvokeUIAction(int keyCode, double time, bool isPsuh)
        {
            km.KeyIndex = keyCode;

            if(km.KeyIndex >= 0)
            {
                UIElement uie = GridKeyLayout.Children[km.KeyIndex];
                Border bdr = (Border)uie;
                if (isPsuh)
                {
                    bdr.Background = Brushes.Red;
                }
                else
                {
                    bdr.Background = Brushes.LightGray;
                }
            }

            string timeStr = Ms2Str(time);

            tbxLog.AppendText($"[{timeStr}] {keyCode} , {isPsuh}\n");
            tbxLog.ScrollToEnd(); // 스크롤 아래로
        }


        string Ms2Str(double time)
        {
            string result = "";

            int h = (int)(time / (1000 * 60 * 60));
            time -= h * (1000 * 60 * 60);
            int m = (int)(time / (1000 * 60));
            time -= m *     (1000 * 60);
            int s = (int)(time / (1000));
            time -= s *     (1000);

            result = h.ToString("00") + ":" +
                m.ToString("00") + ":" +
                s.ToString("00") + ":" +
                time.ToString("000");

            return result;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

            if (!isThreadRun) {
                if (RunThread == null)
                {
                    RunThread = new Thread(() => RunAct());
                    RunThread.IsBackground = true;

                    string nowTime = DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss");
                    MessageTextBlock(txtMessage, $"[{nowTime}] Thread START\n");

                    btnStart.Content = "Pause";
                    
                    RunThread.Start();

                    isThreadRun = true;
                }
                else
                {
                    MessageTextBlock(txtMessage, "[경고] 이미 실행중입니다. - START");
                }
            }
            else
            {
                if (RunThread != null)
                {
                    isThreadRun = false;

                    isTimeSave = true;

                    ThreadExit(RunThread, true);

                    string nowTime = DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss");
                    MessageTextBlock(txtMessage, $"[{nowTime}] Thread PAUSE ({pauseTime})\n");

                    btnStart.Content = "Start";
                }
                else
                {
                    MessageTextBlock(txtMessage, "[경고] 실행중이 아닙니다. - PAUSE");
                }
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (RunThread != null)
            {
                isTimeSave = false;

                ThreadExit(RunThread, true);

                string nowTime = DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss");
                MessageTextBlock(txtMessage, $"[{nowTime}] Thread STOP\n");

                btnStart.Content = "Start";

                isThreadRun = false;
            }
            else
            {
                MessageTextBlock(txtMessage, "[경고] 실행중이 아닙니다. - STOP");
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Log file (*.kjs; *.txt)|*.kjs|Text file (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, tbxLog.Text);

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("로그를 지우겠습니까?", "알림", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                tbxLog.Text = "";
                string nowTime = DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss");
                MessageTextBlock(txtMessage, $"[{nowTime}] Log Clear");
            }
            else
            {
                MessageTextBlock(txtMessage, $"[경고] 클리어 취소 - CLEAR");
            }
        }

        void MessageTextBlock(TextBlock tbx, string str)
        {
            tbx.Text = str;
        }
    }
}
