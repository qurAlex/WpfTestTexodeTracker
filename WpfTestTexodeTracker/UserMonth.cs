using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfTestTexodeTracker
{
    public class UserMonth:INotifyPropertyChanged
    {
        private string _user;
        private int _upperSteps;
        private int _lowerSteps;
        private int _middleSteps;
        private int[] _steps;
        private int[] _rank;
        private string[] _status;

        public UserMonth() 
        {
            _steps = new int[30];
            _rank = new int[30];
            _status = new string[30];
        }

        public UserMonth(string user,int i,  int steps, int rank, string status)
        {
            _steps = new int[30];
            _rank = new int[30];
            _status = new string[30];

            _user = user;
            _steps[i] = steps;
            _rank[i] = rank;
            _status[i] = status;
            
        }

        public string User
        {
            get { return _user; }
            set{ _user = value; OnPropertyChanged("User"); }
        }

        public int UpperSteps
        {
            get { return _upperSteps; }
            set { _upperSteps = value; OnPropertyChanged("UpperSteps"); }
        }

        public int LowerSteps
        {
            get { return _lowerSteps; }
            set { _lowerSteps = value; OnPropertyChanged("LowerSteps"); }
        }

        public int MiddleSteps
        {
            get { return _middleSteps; }
            set { _middleSteps = value; OnPropertyChanged("MiddleSteps"); }
        }

        public int[] Steps
        {
            get { return _steps; }
            set { _steps = value; }
        }

        public int[] Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public string[] Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
