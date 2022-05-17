using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ZdravoKlinika.View.PatientPages.ViewModel
{
    public class RadioButtonDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //Group name then Name
            int[] retVal = new int[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                if (!(values[i].ToString().Equals("")))
                {
                    retVal[i] = Int32.Parse(values[i].ToString().Substring(1));
                }


            }
            return retVal;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

