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

        private static string[] _keyname_array =
        {
            "ESC", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",

            "`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=", "Backspace",
            "Tab", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[", "]", @"\",
            "CapsLock", "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "'", "Enter",
            "LShift", "Z", "X", "C", "V", "B", "N", "M", ",", ".", "/", "RShift",
            "LCtrl", "LWin", "LAlt", "Spacebar", "RAlt", "RWin", "Menu", "RCtrl",

            "PrtSc", "ScrLk", "Pause",
            "Insert", "Home", "Page\nUp", "Delete", "End", "Page\nDown",

            "↑", "←", "↓", "→",

            "Num\nLock", "Num/", "Num*", "Num-",
            "Num7", "Num8", "Num9", "Num+",
            "Num4", "Num5", "Num6",
            "Num1", "Num2", "Num3", "Enter",
            "Num0", "Num."
        };

        private static int[] _keycode_array =
        {
            27, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123,

            192, 49, 50, 51, 52, 53, 54, 55, 56, 57, 48, 189, 187, 8,
            9, 81, 87, 69, 82, 84, 89, 85, 73, 79, 80, 219, 221, 220,
            20, 65, 83, 68, 70, 71, 72, 74, 75, 76, 186, 222, 13,
            160, 90, 88, 67, 86, 66, 78, 77, 188, 190, 191, 161,
            162, 91, 164, 32, 21, 92, 93, 25,

            44, 145, 19,
            45, 36, 33, 46, 35, 34,

            38, 37, 40, 39,

            144, 111, 106, 109,
            103, 104, 105, 107,
            100, 101, 102,
            97, 98, 99, 13,
            96, 110
        };

        
        private static int[] _numlock_on_array =
        {
            103, 104, 105,
            100, 101, 102,
            97, 98, 99,
            96,     110
        };
        private static int[] _numlock_off_array =
        {
            36, 38, 33,
            37, 12, 39,
            35, 40, 34,
            45,     46
        };

        private static int[] _swap_idx_array = new int[NUMBERPAD_COUNT];


        public const int SHIFT_KEYCODE = 16;
        public const int CTRL_KEYCODE = 17;
        public const int ALT_KEYCODE = 18;

        public const int CAPSLOCK_KEYCODE = 20;
        public const int NUMLOCK_KEYCODE = 144;
        public const int SCROLLLOCK_KEYCODE = 145;

        public const int ALPHABET_COUNT = 26;
        public const int NUMBERPAD_COUNT = 11;

        


        public static int[] GetKeyCodeArray()
        {
            return _keycode_array;
        }

        public static string[] GetKeyNameArray()
        {
            return _keyname_array;
        }

        public static int[] GetAlphabetIndexArray()
        {
            int[] result = new int[ALPHABET_COUNT];
            int indexCount = 0;

            for (int i = 0; i < _keycode_array.Length && indexCount < ALPHABET_COUNT; i++)
            {
                if ('A' <= _keycode_array[i] && _keycode_array[i] <= 'Z')
                {
                    result[indexCount++] = i;
                }
            }

            return result;
        }

        public static int[] GetNumpadIndexArray()
        {
            return _swap_idx_array;
        }

        public static int GetKeyIndex(int _keyCode)
        {
            for (int i = 0; i < _keycode_array.Length; i++)
            {
                if (_keycode_array[i] == _keyCode)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int GetKeyCode(int _idx)
        {
            return _keycode_array[_idx];
        }

        public static string GetKeyName(int _keyCode)
        {
            for (int i = 0; i < _keyname_array.Length; i++)
            {
                if (_keycode_array[i] == _keyCode)
                {
                    return _keyname_array[i];
                }
            }

            return $"<{_keyCode}>";
        }

        public static string GetKeyNameToIndex(int _idx)
        {
            return _keyname_array[_idx];
        }

        public static void SaveNumkeyIndex()
        {
            int _cnt = 0;

            for (int i = 0; i < _keycode_array.Length || _cnt < NUMBERPAD_COUNT; i++)
            {
                if(_keycode_array[i] == _numlock_on_array[_cnt])
                {
                    _swap_idx_array[_cnt++] = i;
                }
            }
        }

        public static void SwapNumKeyCodeArray(bool _isState)
        {
            if (_isState)
            {
                for (int i = 0; i < NUMBERPAD_COUNT; i++)
                {
                    int idx = _swap_idx_array[i];
                    _keycode_array[idx] = _numlock_on_array[i];
                }
            }
            else
            {
                for (int i = 0; i < NUMBERPAD_COUNT; i++)
                {
                    int idx = _swap_idx_array[i];
                    _keycode_array[idx] = _numlock_off_array[i];
                }
            }
            
        }
    }
}
