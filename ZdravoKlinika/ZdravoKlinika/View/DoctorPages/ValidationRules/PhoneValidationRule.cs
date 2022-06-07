using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace ZdravoKlinika.View.DoctorPages.ValidationRules
{
    public class PhoneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult validationResult;
            string val = (string)value;
            string pattern1 = "^06[\\d]{8}\\z";
            string pattern2 = "^\\+[\\d]{12}\\z";

            if (Regex.IsMatch(val, pattern1) || Regex.IsMatch(val, pattern2))
            {
                validationResult = new ValidationResult(true, null);
            }
            else if (Regex.IsMatch(val.Replace('+', ' ').TrimStart(), "\\D"))
            {
                validationResult = new ValidationResult(false, "Nedozvoljeni karakter.");
            }
            else if ((val.StartsWith('0') && val.Length < 10) || (val.StartsWith('+') && val.Length < 13))
            {
                validationResult = new ValidationResult(false, "Nedovoljan broj karaktera.");
            }
            else if ((val.StartsWith('0') && val.Length > 10) || (val.StartsWith('+') && val.Length > 13))
            {
                validationResult = new ValidationResult(false, "Višak karaktera.");
            } else
            {
                validationResult = new ValidationResult(false, "Nepoznat format broja.");
            }

            return validationResult;
        }
    }
}
