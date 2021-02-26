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
        Thread RunMainThread = null;

        private double pauseTime = 0;

        private bool isThreadEnding = false;
        private bool isThreadRun = false;
        private bool isTimeSave = false;


        [DllImport("user32.dll")]
        public static extern UInt16 GetAsyncKeyState(Int32 vKey);
        public MainWindow()
        {
            InitializeComponent();

            UI_Update(true, true);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!isThreadRun)
            {
                if (RunMainThread == null)
                {
                    RunMainThread = new Thread(() => RunAct());
                    RunMainThread.IsBackground = true;

                    UI_Update(true, true);
                    string nowTime = DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss");
                    MessageTextBlock(txtMessage, $"[{nowTime}] Thread START\n");

                    btnStart.Content = "Pause";

                    RunMainThread.Start();

                    isThreadRun = true;
                }
                else
                {
                    MessageTextBlock(txtMessage, "[경고] 이미 실행중입니다. - START");
                }
            }
            else
            {
                if (RunMainThread != null)
                {
                    isThreadRun = false;

                    isTimeSave = true;

                    ThreadExit(ref RunMainThread, true);

                    UI_Update(true, true);
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
            if (RunMainThread != null)
            {
                isTimeSave = false;

                ThreadExit(ref RunMainThread, true);

                UI_Update(true, true);
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
            if (MessageBox.Show("로그를 지우겠습니까?", "알림", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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



        void RunAct()
        {
            bool[] isPrevPush = new bool[0xFE];

            for (int i = 0; i < 0xFE; i++)
            {
                isPrevPush[i] = false;
            }

            Stopwatch sw = new Stopwatch();

            sw.Start();

            try
            {
                while (true)
                {
                    for (int _keyCode = 1; _keyCode < 0xFE; _keyCode++)
                    {
                        switch (_keyCode) // shift, ctrl, alt 누르면 키코드 두개 입력되지 않도록 continue
                        {
                            case KeyModel.SHIFT_KEYCODE:
                            case KeyModel.CTRL_KEYCODE:
                            case KeyModel.ALT_KEYCODE:
                                continue;
                        }

                        int current = GetAsyncKeyState(_keyCode);

                        bool isPush = (current > 1) ? true : false;

                        if (isPush != isPrevPush[_keyCode]) // 키 눌림상태 이전과 현재가 다를 경우.
                        {
                            this.Invoke(new Action(() => InvokeUIAction_LogInput(_keyCode, sw.ElapsedMilliseconds + pauseTime, isPush)));

                            if (!isPush) // 키를 놓을 경우에
                            {
                                switch (_keyCode)
                                {
                                    case KeyModel.CAPSLOCK_KEYCODE:
                                    case KeyModel.NUMLOCK_KEYCODE:
                                        this.Invoke(new Action(() => InvokeUIAction_LockKey(_keyCode)));
                                        break;
                                    case KeyModel.SCROLLLOCK_KEYCODE:
                                        break;
                                }
                            }

                            isPrevPush[_keyCode] = isPush; // 현재 키 상태를 이전 상태에 저장.
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

        void InvokeUIAction_LogInput(int _keyCode, double _time, bool _isPush)
        {
            var keyidx = KeyModel.KeyCode2Index(_keyCode);

            for(int i = 0; i < keyidx.Count; i++)
            {
                int idx = keyidx[i];
                if (idx > KeyModel.ERROR_INT)
                {
                    Border bdr = GetBorder(idx);
                    TextBlock tbx = GetTextBlock(idx);

                    UI_ColorUpdate(bdr, tbx, _isPush);
                }
            }

            string timeStr = Ms2Str(_time);

            string keyName = KeyModel.KeyCode2KeyName(_keyCode);

            string isPushStr = _isPush ? "PUSH" : "PULL";

            tbxLog.AppendText($"[{timeStr}] [{isPushStr}]\t {keyName}\n");
            tbxLog.ScrollToEnd(); // 스크롤 아래로
        }


        void InvokeUIAction_LockKey(int keyCode)
        {
            switch (keyCode)
            {
                case KeyModel.CAPSLOCK_KEYCODE:
                    {
                        bool isState = Keyboard.IsKeyToggled(Key.CapsLock);

                        KeyModel.CapsUpdate(isState);

                        // 해당범위 UI 업데이트
                        UI_Update(true, true, KeyModel.GetCapsIndexArray());

                        break;
                    }
                case KeyModel.NUMLOCK_KEYCODE:
                    {
                        bool isState = Keyboard.IsKeyToggled(Key.NumLock);

                        KeyModel.NumUpdate(isState);

                        // 해당범위 UI 업데이트

                        UI_Update(true, true, KeyModel.GetNumIndexArray(true));
                        UI_Update(true, true, KeyModel.GetNumIndexArray(false));

                        break;
                    }
                case KeyModel.SCROLLLOCK_KEYCODE:
                    break;
            }
        }

        void UI_Update(bool _isTextUpdate, bool _isColorUpdate)
        {
            int uiCount = GridKeyLayout.Children.Count;

            int[] idx_range_arr = new int[uiCount];

            for(int i=0; i<uiCount; i++)
            {
                idx_range_arr[i] = i;
            }

            UI_Update(_isTextUpdate, _isColorUpdate, idx_range_arr);
        }

        void UI_Update(bool _isTextUpdate, bool _isColorUpdate, int[] _idx_range)
        {
            for (int i = 0; i < _idx_range.Length; i++)
            {
                int idx = _idx_range[i];

                if (idx >= GridKeyLayout.Children.Count) continue;

                string keyview = KeyModel.GetKeyView(idx);

                TextBlock tbx = GetTextBlock(idx);

                if (_isTextUpdate)
                {
                    tbx.Text = keyview;
                }

                if (_isColorUpdate)
                {
                    Border bdr = GetBorder(idx);
                    UI_ColorUpdate(bdr, tbx);
                }
            }
        }

        void UI_ColorUpdate(Border _bdr, TextBlock _tbx)
        {
            _bdr.Background = LayoutModel.PullBackColor;
            _tbx.Foreground = LayoutModel.PullFontColor;
        }

        void UI_ColorUpdate(Border _bdr, TextBlock _tbx, bool _isPush)
        {
            if (_isPush)
            {
                _bdr.Background = LayoutModel.PushBackColor;
                _tbx.Foreground = LayoutModel.PushFontColor;
            }
            else
            {
                UI_ColorUpdate(_bdr, _tbx);
            }
        }


        void MessageTextBlock(TextBlock tbx, string str)
        {
            tbx.Text = str;
        }

        string Ms2Str(double time)
        {
            string result = "";

            int h = (int)(time / (1000 * 60 * 60));
            time -= h * (1000 * 60 * 60);
            int m = (int)(time / (1000 * 60));
            time -= m * (1000 * 60);
            int s = (int)(time / (1000));
            time -= s * (1000);

            result = h.ToString("00") + ":" +
                m.ToString("00") + ":" +
                s.ToString("00") + ":" +
                time.ToString("000");

            return result;
        }

        void ThreadExit(ref Thread tr, bool isDelay)
        {
            tr.Interrupt();

            tr = null;

            while (isThreadEnding == false && isDelay)
            {
                Thread.Sleep(1);
            }

            isThreadEnding = false;
        }

        Border GetBorder(int _idx)
        {
            if (_idx < GridKeyLayout.Children.Count)
            {
                return (Border)GridKeyLayout.Children[_idx];
            }
            return new Border();
        }

        TextBlock GetTextBlock(int _idx)
        {
            if (_idx < GridKeyLayout.Children.Count)
            {
                return (TextBlock)GetBorder(_idx).Child;
            }
            return new TextBlock();
        }
    }
}
