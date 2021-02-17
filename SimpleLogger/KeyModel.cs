using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SimpleLogger
{
    public static class KeyModel
    {
        //기능키	13
        //메인키	14+14+13+12+8
        //서브키	6+3
        //방향키	4
        //넘버키	17

        //총104키

        //private static string[] _keyname_array =
        //{
        //    "ESC", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",

        //    "`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=", "Backspace",
        //    "Tab", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[", "]", @"\",
        //    "CapsLock", "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "'", "Enter",
        //    "LShift", "Z", "X", "C", "V", "B", "N", "M", ",", ".", "/", "RShift",
        //    "LCtrl", "LWin", "LAlt", "Spacebar", "RAlt", "RWin", "Menu", "RCtrl",

        //    "PrtSc", "ScrLk", "Pause",
        //    "Insert", "Home", "Page\nUp", "Delete", "End", "Page\nDown",

        //    "↑", "←", "↓", "→",

        //    "Num\nLock", "Num/", "Num*", "Num-",
        //    "Num7", "Num8", "Num9", "Num+",
        //    "Num4", "Num5", "Num6",
        //    "Num1", "Num2", "Num3", "Enter",
        //    "Num0", "Num."
        //};

        //private static int[] _keycode_array =
        //{
        //    27, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123,

        //    192, 49, 50, 51, 52, 53, 54, 55, 56, 57, 48, 189, 187, 8,
        //    9, 81, 87, 69, 82, 84, 89, 85, 73, 79, 80, 219, 221, 220,
        //    20, 65, 83, 68, 70, 71, 72, 74, 75, 76, 186, 222, 13,
        //    160, 90, 88, 67, 86, 66, 78, 77, 188, 190, 191, 161,
        //    162, 91, 164, 32, 21, 92, 93, 25,

        //    44, 145, 19,
        //    45, 36, 33, 46, 35, 34,

        //    38, 37, 40, 39,

        //    144, 111, 106, 109,
        //    103, 104, 105, 107,
        //    100, 101, 102,
        //    97, 98, 99, 13,
        //    96, 110
        //};

        public static KeyTable[] kt = new KeyTable[KEY_COUNT];
        public static KeyTable[] ktNumOn = new KeyTable[CAPS_COUNT];
        public static KeyTable[] ktNumOff = new KeyTable[CAPS_COUNT];
        private static readonly int[] capsIdxArray = new int[CAPS_COUNT];

        public const int KEY_COUNT = 105;
        public const int CAPS_COUNT = 26;
        public const int NUM_COUNT = 11;

        public const int SHIFT_KEYCODE = 16;
        public const int CTRL_KEYCODE = 17;
        public const int ALT_KEYCODE = 18;

        public const int CAPSLOCK_KEYCODE = 20;
        public const int NUMLOCK_KEYCODE = 144;
        public const int SCROLLLOCK_KEYCODE = 145;

        public const int ERROR_INT = -1;
        public const string ERROR_STR = "";


        static KeyModel()
        {
            int _idx = 0;
            int _num_on_idx = 0;
            int _num_off_idx = 0;

            // KEYCODE , KEYNAME, KEYVIEW
            kt[_idx++] = new KeyTable(27, "ESC");
            kt[_idx++] = new KeyTable(112, "F1");
            kt[_idx++] = new KeyTable(113, "F2");
            kt[_idx++] = new KeyTable(114, "F3");
            kt[_idx++] = new KeyTable(115, "F4");
            kt[_idx++] = new KeyTable(116, "F5");
            kt[_idx++] = new KeyTable(117, "F6");
            kt[_idx++] = new KeyTable(118, "F7");
            kt[_idx++] = new KeyTable(119, "F8");
            kt[_idx++] = new KeyTable(120, "F9");
            kt[_idx++] = new KeyTable(121, "F10");
            kt[_idx++] = new KeyTable(122, "F11");
            kt[_idx++] = new KeyTable(123, "F12");
            kt[_idx++] = new KeyTable(192, "`");
            kt[_idx++] = new KeyTable(49, "1");
            kt[_idx++] = new KeyTable(50, "2");
            kt[_idx++] = new KeyTable(51, "3");
            kt[_idx++] = new KeyTable(52, "4");
            kt[_idx++] = new KeyTable(53, "5");
            kt[_idx++] = new KeyTable(54, "6");
            kt[_idx++] = new KeyTable(55, "7");
            kt[_idx++] = new KeyTable(56, "8");
            kt[_idx++] = new KeyTable(57, "9");
            kt[_idx++] = new KeyTable(48, "0");
            kt[_idx++] = new KeyTable(189, "-");
            kt[_idx++] = new KeyTable(187, "=");
            kt[_idx++] = new KeyTable(8, "Backspace");
            kt[_idx++] = new KeyTable(9, "Tab");
            kt[_idx++] = new KeyTable(81, "Q", "q");
            kt[_idx++] = new KeyTable(87, "W", "w");
            kt[_idx++] = new KeyTable(69, "E", "e");
            kt[_idx++] = new KeyTable(82, "R", "r");
            kt[_idx++] = new KeyTable(84, "T", "t");
            kt[_idx++] = new KeyTable(89, "Y", "y");
            kt[_idx++] = new KeyTable(85, "U", "u");
            kt[_idx++] = new KeyTable(73, "I", "i");
            kt[_idx++] = new KeyTable(79, "O", "o");
            kt[_idx++] = new KeyTable(80, "P", "p");
            kt[_idx++] = new KeyTable(219, "[");
            kt[_idx++] = new KeyTable(221, "]");
            kt[_idx++] = new KeyTable(220, @"\");
            kt[_idx++] = new KeyTable(20, "CapsLock");
            kt[_idx++] = new KeyTable(65, "A", "a");
            kt[_idx++] = new KeyTable(83, "S", "s");
            kt[_idx++] = new KeyTable(68, "D", "d");
            kt[_idx++] = new KeyTable(70, "F", "f");
            kt[_idx++] = new KeyTable(71, "G", "g");
            kt[_idx++] = new KeyTable(72, "H", "h");
            kt[_idx++] = new KeyTable(74, "J", "j");
            kt[_idx++] = new KeyTable(75, "K", "k");
            kt[_idx++] = new KeyTable(76, "L", "l");
            kt[_idx++] = new KeyTable(186, ";");
            kt[_idx++] = new KeyTable(222, "'");
            kt[_idx++] = new KeyTable(13, "Enter");
            kt[_idx++] = new KeyTable(160, "LShift", "Shift");
            kt[_idx++] = new KeyTable(90, "Z", "z");
            kt[_idx++] = new KeyTable(88, "X", "x");
            kt[_idx++] = new KeyTable(67, "C", "c");
            kt[_idx++] = new KeyTable(86, "V", "v");
            kt[_idx++] = new KeyTable(66, "B", "b");
            kt[_idx++] = new KeyTable(78, "N", "n");
            kt[_idx++] = new KeyTable(77, "M", "m");
            kt[_idx++] = new KeyTable(188, ",");
            kt[_idx++] = new KeyTable(190, ".");
            kt[_idx++] = new KeyTable(191, "/");
            kt[_idx++] = new KeyTable(161, "RShift", "Shift");
            kt[_idx++] = new KeyTable(162, "LCtrl", "Ctrl");
            kt[_idx++] = new KeyTable(91, "LWin", "Win");
            kt[_idx++] = new KeyTable(164, "LAlt", "Alt");
            kt[_idx++] = new KeyTable(32, "Spacebar");
            kt[_idx++] = new KeyTable(21, "RAlt", "Alt");
            kt[_idx++] = new KeyTable(92, "RWin", "Win");
            kt[_idx++] = new KeyTable(93, "Menu");
            kt[_idx++] = new KeyTable(25, "RCtrl", "Ctrl");
            kt[_idx++] = new KeyTable(44, "PrtSc");
            kt[_idx++] = new KeyTable(145, "ScrLk");
            kt[_idx++] = new KeyTable(19, "Pause");
            kt[_idx++] = new KeyTable(45, "Insert", "Ins");
            kt[_idx++] = new KeyTable(36, "Home");
            kt[_idx++] = new KeyTable(33, "PageUp", "Page\nUp");
            kt[_idx++] = new KeyTable(46, "Delete", "Del");
            kt[_idx++] = new KeyTable(35, "End");
            kt[_idx++] = new KeyTable(34, "PageDown", "Page\nDown");
            kt[_idx++] = new KeyTable(38, "↑");
            kt[_idx++] = new KeyTable(37, "←");
            kt[_idx++] = new KeyTable(40, "↓");
            kt[_idx++] = new KeyTable(39, "→");
            kt[_idx++] = new KeyTable(144, "NumLock", "Num\nLock");
            kt[_idx++] = new KeyTable(111, "Num/", "/");
            kt[_idx++] = new KeyTable(106, "Num*", "*");
            kt[_idx++] = new KeyTable(109, "Num-", "-");
            kt[_idx++] = new KeyTable(103, "Num7", "7");
            kt[_idx++] = new KeyTable(104, "Num8", "8");
            kt[_idx++] = new KeyTable(105, "Num9", "9");
            kt[_idx++] = new KeyTable(107, "Num+", "+");
            kt[_idx++] = new KeyTable(100, "Num4", "4");
            kt[_idx++] = new KeyTable(101, "Num5", "5");
            kt[_idx++] = new KeyTable(102, "Num6", "6");
            kt[_idx++] = new KeyTable(97, "Num1", "1");
            kt[_idx++] = new KeyTable(98, "Num2", "2");
            kt[_idx++] = new KeyTable(99, "Num3", "3");
            kt[_idx++] = new KeyTable(13, "Enter");
            kt[_idx++] = new KeyTable(96, "Num0", "0");
            kt[_idx++] = new KeyTable(110, "Num.", ".");
            kt[_idx++] = new KeyTable(12, "CLEAR");

            ktNumOn[_num_on_idx++] = GetKeyTable(103);
            ktNumOn[_num_on_idx++] = GetKeyTable(104);
            ktNumOn[_num_on_idx++] = GetKeyTable(105);
            ktNumOn[_num_on_idx++] = GetKeyTable(100);
            ktNumOn[_num_on_idx++] = GetKeyTable(101);
            ktNumOn[_num_on_idx++] = GetKeyTable(102);
            ktNumOn[_num_on_idx++] = GetKeyTable(97);
            ktNumOn[_num_on_idx++] = GetKeyTable(98);
            ktNumOn[_num_on_idx++] = GetKeyTable(99);
            ktNumOn[_num_on_idx++] = GetKeyTable(96);
            ktNumOn[_num_on_idx++] = GetKeyTable(110);

            ktNumOff[_num_off_idx++] = GetKeyTable(36);
            ktNumOff[_num_off_idx++] = GetKeyTable(38);
            ktNumOff[_num_off_idx++] = GetKeyTable(33);
            ktNumOff[_num_off_idx++] = GetKeyTable(37);
            ktNumOff[_num_off_idx++] = GetKeyTable(12);
            ktNumOff[_num_off_idx++] = GetKeyTable(39);
            ktNumOff[_num_off_idx++] = GetKeyTable(35);
            ktNumOff[_num_off_idx++] = GetKeyTable(40);
            ktNumOff[_num_off_idx++] = GetKeyTable(34);
            ktNumOff[_num_off_idx++] = GetKeyTable(45);
            ktNumOff[_num_off_idx++] = GetKeyTable(46);

            SetCapsIdxArray();
        }


        private static void SetCapsIdxArray()
        {
            int cc = 0;

            for (int i = 0; i < KEY_COUNT && cc < CAPS_COUNT; i++)
            {
                int keycode = kt[i].KeyCode;

                if ('A' <= keycode && keycode <= 'Z')
                {
                    capsIdxArray[cc] = i;
                    cc++;
                }
            }
        }

        public static int[] GetKeyCodeArray()
        {
            int[] result = new int[KEY_COUNT];

            for(int i=0; i<KEY_COUNT; i++)
            {
                result[i] = kt[i].KeyCode;
            }

            return result;
        }

        public static string[] GetKeyNameArray()
        {
            string[] result = new string[KEY_COUNT];

            for (int i = 0; i < KEY_COUNT; i++)
            {
                result[i] = kt[i].KeyName;
            }

            return result;
        }

        public static string[] GetKeyViewArray()
        {
            string[] result = new string[KEY_COUNT];

            for (int i = 0; i < KEY_COUNT; i++)
            {
                result[i] = kt[i].KeyView;
            }

            return result;
        }

        // 리스트로 변경, 하나의 키코드가 두개 이상 존재함.
        public static List<int> KeyCode2Index(int _keycode)
        {
            List<int> result = new List<int>();

            for(int i=0; i<KEY_COUNT; i++)
            {
                if (kt[i].KeyCode == _keycode)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        // 리스트로 변경, 하나의 키네임가 두개 이상 존재함.
        public static List<int> KeyName2Index(string _keyname)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < KEY_COUNT; i++)
            {
                if (kt[i].KeyName == _keyname)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        // 리스트로 변경, 하나의 키뷰가 두개 이상 존재함.
        public static List<int> KeyView2Index(string _keyview)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < KEY_COUNT; i++)
            {
                if (kt[i].KeyView == _keyview)
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public static int GetKeyCode(int _idx)
        {
            if (_idx < KEY_COUNT) return kt[_idx].KeyCode;
            else return ERROR_INT;
        }

        public static string GetKeyName(int _idx)
        {
            if (_idx < KEY_COUNT) return kt[_idx].KeyName;
            else return ERROR_STR;
        }

        public static string GetKeyView(int _idx)
        {
            if (_idx < KEY_COUNT) return kt[_idx].KeyView;
            else return ERROR_STR;
        }

        // 결과가 두개이상 있을 수 있지만 크게 문제되지 않으므로 유지
        public static string KeyCode2KeyName(int _keycode)
        {
            for (int i = 0; i < KEY_COUNT; i++)
            {
                if (kt[i].KeyCode == _keycode) return kt[i].KeyName;
            }

            return ERROR_STR;
        }

        
        // 키코드하나에 키테이블 두개이상 매치가능 있기 떄문에 생성자에만 사용하도록 접근제한자 private로 설정
        private static KeyTable GetKeyTable(int _keycode)
        {
            for(int i = 0; i < KEY_COUNT; i++)
            {
                if (kt[i].KeyCode == _keycode)
                {
                    KeyTable ktResult = new KeyTable(ERROR_INT, ERROR_STR);
                    ktResult.KeyCode = kt[i].KeyCode;
                    ktResult.KeyName = kt[i].KeyName;
                    ktResult.KeyView = kt[i].KeyView;
                    ktResult.Index = i;
                    return ktResult;
                }
            }

            return new KeyTable(ERROR_INT, ERROR_STR, ERROR_STR, ERROR_INT);
        }

        

        // 결과가 2개이상 있을 수 있지만 A to Z 까지는 하나이므로 유지
        public static void CapsUpdate(bool _isState)
        {
            for(int i = 0; i < CAPS_COUNT; i++)
            {
                int idx = capsIdxArray[i];

                if (_isState)
                {
                    kt[idx].KeyView = kt[idx].KeyView.ToUpper();
                }
                else
                {
                    kt[idx].KeyView = kt[idx].KeyView.ToLower();
                }
            }
        }

        public static int[] GetCapsIndexArray()
        {
            return capsIdxArray;
        }


        public static void NumUpdate(bool _isState)
        {
            for (int i = 0; i < NUM_COUNT; i++)
            {
                int idx = ktNumOn[i].Index;

                if (_isState)
                {
                    kt[idx].KeyCode = ktNumOn[i].KeyCode;
                    kt[idx].KeyName = ktNumOn[i].KeyName;
                    kt[idx].KeyView = ktNumOn[i].KeyView;
                }
                else
                {
                    kt[idx].KeyCode = ktNumOff[i].KeyCode;
                    kt[idx].KeyName = ktNumOff[i].KeyName;
                    kt[idx].KeyView = ktNumOff[i].KeyView;
                }
                
            }

        }

        public static int[] GetNumIndexArray(bool _isState)
        {
            int[] result = new int[NUM_COUNT];

            for (int i = 0; i < NUM_COUNT; i++)
            {
                if (_isState)
                {
                    result[i] = ktNumOn[i].Index;
                }
                else
                {
                    result[i] = ktNumOff[i].Index;
                }
            }

            return result;
        }
    }
}
