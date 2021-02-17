using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    public class KeyTable
    {
        private int keycode = KeyModel.ERROR_INT;
        private string keyname = KeyModel.ERROR_STR;
        private string keyview = KeyModel.ERROR_STR;
        private int idx = KeyModel.ERROR_INT;

        public KeyTable(int _keycode, string _keyname)
        {
            this.keycode = _keycode;
            this.keyname = _keyname;
            this.keyview = _keyname;
        }

        public KeyTable(int _keycode, string _keyname, string _keyview)
        {
            this.keycode = _keycode;
            this.keyname = _keyname;
            this.keyview = _keyview;
        }

        public KeyTable(int _keycode, string _keyname, string _keyview, int _index) 
            : this(_keycode, _keyname, _keyview)
        {
            this.idx = _index;
        }


        public int KeyCode
        {
            get { return keycode; }
            set
            {
                if (keycode != value)
                {
                    this.keycode = value;
                }
            }
        }

        public string KeyName
        {
            get { return keyname; }
            set
            {
                if (keyname != value)
                {
                    this.keyname = value;
                }
            }
        }

        public string KeyView
        {
            get { return keyview; }
            set
            {
                if (keyview != value)
                {
                    this.keyview = value;
                }
            }
        }

        public int Index
        {
            get { return idx; }
            set
            {
                if (idx != value)
                {
                    this.idx = value;
                }
            }
        }



    }
}
