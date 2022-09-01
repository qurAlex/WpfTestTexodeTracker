using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfTestTexodeTracker
{
    class DifToColorConverter : IMultiValueConverter
    {
        //по итогу худший и лучший результат берет из выбранного элемента, а не от остальных
        int LowerSteps;
        int UpperSteps;
        UserMonth selectedUser;

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            LowerSteps = (int)value[0];
            UpperSteps = (int)value[1];
            selectedUser = (UserMonth)value[2];

            if (Math.Abs(selectedUser.MiddleSteps - LowerSteps) > selectedUser.MiddleSteps * 0.2 || Math.Abs(selectedUser.MiddleSteps - UpperSteps) > selectedUser.MiddleSteps * 0.2)
                return new SolidColorBrush(Colors.OrangeRed);
            else return new SolidColorBrush(Colors.White);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        
    }
}
