using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimpleLogger
{
    public class KeysModel
    {

        //기능키	13
        //메인키	14+14+13+12+8
        //서브키	6+3
        //방향키	4
        //넘버키	17

        //총104키

        private static readonly int[] KeyCodeArray = 
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

        private static readonly string[] KeyNameArray = 
        {
            "ESC", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",

            "`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=", "Backspace",
            "Tab", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[", "]", @"\", 
            "CapsLock", "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "'", "Enter", 
            "LShift", "Z", "X", "C", "V", "B", "N", "M", ",", ".", "/", "RShift", 
            "LCtrl", "LWin", "LAlt", "Spacebar", "RAlt", "RWin", "Menu", "RCtrl",

            "PrtSc", "ScrLk", "Pause",
            "Insert", "Home", "PageUp", "Delete", "End", "PageDown",

            "↑", "←", "↓", "→",

            "NumLock", "Num/", "Num*", "Num-",
            "Num7", "Num8", "Num9", "Num+",
            "Num4", "Num5", "Num6",
            "Num1", "Num2", "Num3", "Enter",
            "Num0", "Num."
        };

        private int _keyCode = -1;
        private bool _isNumLock = false;
        private bool _isCapsLock = false;
        private bool _isScrollLock = false;

        public int SetKeyCode
        {
            set
            {
                _keyCode = value;
            }
        }

        public int GetKeyIndex
        {
            get
            {
                for (int i = 0; i < KeyCodeArray.Length; i++)
                {
                    if (KeyCodeArray[i] == _keyCode)
                    {
                        return i;
                    }
                }
                return -1;
            }
        }

        public string GetKeyName
        {
            get
            {
                for (int i = 0; i < KeyNameArray.Length; i++)
                {
                    if (KeyCodeArray[i] == _keyCode)
                    {
                        return KeyNameArray[i];
                    }
                }
                return "<null>";
            }
        }

        public bool IsNumLock
        {
            get
            {
                return _isNumLock;
            }
            set
            {
                if(_isNumLock != value)
                {
                    _isNumLock = value;
                }
                
            }
        }

        public bool IsCapsLock
        {
            get
            {
                return _isCapsLock;
            }
            set
            {
                if (_isCapsLock != value)
                {
                    _isCapsLock = value;
                }

            }
        }

        public bool IsScrollLock
        {
            get
            {
                return _isScrollLock;
            }
            set
            {
                if (_isScrollLock != value)
                {
                    _isScrollLock = value;
                }

            }
        }


    }
}
