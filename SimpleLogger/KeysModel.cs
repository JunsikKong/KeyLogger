using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimpleLogger
{
    public class KeysModel
    {

        private static readonly int[] KeyCodeArray
            = { 27, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123,

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

        private int _keyCode = -1;


        public int KeyIndex
        {
            set
            {
                _keyCode = value;
            }
            get
            {
                for(int i = 0; i < KeyCodeArray.Length; i++)
                {
                    if(KeyCodeArray[i] == _keyCode)
                    {
                        return i;
                    }
                }
                return -1;
            }
        }
    }
}
