using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfTestTexodeTracker
{
    public class Users
    {
        private int _rank;
        private string _user;
        private string _status;
        private int _steps;

        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public string User
        {
            get { return _user; }
            set { _user = value;}
        }

        public string Status
        {
            get { return _status; }
            set { _status = value;  }
        }

        public int Steps
        {
            get { return _steps; }
            set { _steps = value; }
        }

      
    }
}
