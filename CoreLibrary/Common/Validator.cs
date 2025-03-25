using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreLibrary
{
    public static class Validator
    {
        //public static bool IsTextOnly(String inputText)
        //{
        //    if (String.IsNullOrEmpty(inputText))
        //    {
        //        return false;
        //    }
        //    return Regex.IsMatch(inputText, @"^[a-zA-Z\s]+$");
        //}

        public static bool IsNumericOnly(object inputNumber)
        {
            try
            {
                if (inputNumber == null || inputNumber is DateTime)
                {
                    return false;
                }

                if (inputNumber is Int16 || inputNumber is Int32 || inputNumber is Int64 || inputNumber is Decimal ||
                    inputNumber is Single || inputNumber is Double || inputNumber is Boolean)
                {
                    return true;
                }

                try
                {
                    if (inputNumber is string)
                        Double.Parse(inputNumber as string);
                    else
                        Double.Parse(inputNumber.ToString());
                    return true;
                }
                catch
                {
                }
                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsIntNumberOnly(String inputNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(inputNumber))
                {
                    return false;
                }
                else
                {
                    Regex isnumber = new Regex("[^0-9]");
                    return !isnumber.IsMatch(inputNumber);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsValidEmail(String inputMail)
        {
            try
            {
                if (string.IsNullOrEmpty(inputMail))
                {
                    return false;
                }
                else
                {
                    Regex regex =
                        new Regex(
                            @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
                    Match match = regex.Match(inputMail);
                    if (match.Success)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool IsValidPhoneNo(String inputPhoneNo)
        {
            try
            {
                if (string.IsNullOrEmpty(inputPhoneNo))
                {
                    return false;
                }
                else
                {
                    string regex = @"((\(\d{3}\) ?)|(\d{3}[- \.]))?\d{3}[- \.]\d{4}(\s(x\d+)?){0,1}$";
                    if (Regex.IsMatch(inputPhoneNo, regex))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsMaximum(object inputText, int length)
        {
            try
            {

                if (string.IsNullOrEmpty(inputText.ToString()))
                {
                    return false;
                }
                else
                {
                    int stringLength = inputText.ToString().Length;
                    if (stringLength <= length)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsMinimum(object inputText, int length)
        {
            try
            {
                if (string.IsNullOrEmpty(inputText.ToString()))
                {
                    return false;
                }
                else
                {
                    int stringLength = inputText.ToString().Length;
                    if (stringLength >= length)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool IsOddNumber(object inputText)
        {
            try
            {
                if (string.IsNullOrEmpty(inputText.ToString()))
                {
                    return false;
                }
                int number = Convert.ToInt32(inputText);
                int result = number % 2;
                if (result == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsEvenNumber(object inputText)
        {
            try
            {
                if (string.IsNullOrEmpty(inputText.ToString()))
                {
                    return false;
                }
                int number = Convert.ToInt32(inputText);
                int result = number % 2;
                if (result == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsNonNegative(object inputText)
        {
            try
            {
                if (string.IsNullOrEmpty(inputText.ToString()))
                {
                    return false;
                }
                double number = Convert.ToDouble(inputText);
                if (number >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsValidBDMobileNo(String mobileNo)
        {
            try
            {
                if (string.IsNullOrEmpty(mobileNo))
                {
                    return false;
                }
                else
                {
                    string regex = @"^(?:\+88|01)?(?:\d{11}|\d{13})$";
                    if (Regex.IsMatch(mobileNo, regex))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsValidDate(String date)
        {
            try
            {
                if (string.IsNullOrEmpty(date))
                {
                    return false;
                }
                else
                {
                    string regex =
                        "(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]((175[7-9])|(17[6-9][0-9])|(1[8-9][0-9][0-9])|([2-9][0-9][0-9][0-9]))";
                    if (Regex.IsMatch(date, regex))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool UserIDCheck(String UserId)
        {
            if (UserId.Length <= 10 && UserId.Length > 2)
            {

                if (UserId.Trim() != "")
                {

                    Match rex = Regex.Match(UserId.Trim(' '), "^[a-zA-Z0-9 ]*$", RegexOptions.IgnoreCase);

                    if (rex.Success == false)
                    {
                        return false;
                    }

                    else
                    {
                        return true;
                    }

                }

                else
                {
                    return false;
                }
            }
            else
            {
                return false;

            }

        }

        public static bool CheckPasswordCritra(String Password)
        {
            bool TF = false;
            if (Password.Trim().Length >= 6 && Password.Trim().ToString() != ".")
            {
                foreach (char c in Password)
                {
                    if (char.IsUpper(c))
                    {
                        TF = true;
                    }
                }

                if (TF)
                {
                    return true;
                }
                else
                {
                    return TF;
                }

            }
            else
            {
                return TF;
            }
        }
    }

    public class ValidationProcess
    {
        public Boolean IsValid { set; get; } = true;
        public String Message { set; get; }
    }
}
