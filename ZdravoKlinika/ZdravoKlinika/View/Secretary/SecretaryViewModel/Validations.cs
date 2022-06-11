using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public class TimeValidation : ValidationRule
    {
        public TimeValidation()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String a = value.ToString();
            Regex regex = new Regex("[0-9]{1,2}[:][0-9]{2}");
            if (!regex.IsMatch(a))
            {
                return new ValidationResult(false, $"Vreme mora biti u formati hh:mm");

            }

            int hours = Int32.Parse(a.Split(":")[0]);
            int minutes = Int32.Parse(a.Split(":")[1]);
            if (hours >= 24 || minutes >= 60)
                return new ValidationResult(false, $"Sat < 24 i minuti < 60");

            return ValidationResult.ValidResult;

        }
    }
    public class EmptyValidation : ValidationRule
    {
        public EmptyValidation()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String a = value.ToString();
            if(a == "")
                return new ValidationResult(false, $"Vrednost mora biti popunjena");


            return ValidationResult.ValidResult;

        }
    }
    public class NumberValidation : ValidationRule
    {
        public NumberValidation()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int ab = Int32.Parse((string)value);
            }
            catch 
            {
                return new ValidationResult(false, $"Vrednost mora biti broj");
            }
            return ValidationResult.ValidResult;

        }
    }
}
