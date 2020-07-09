
using System;

using System.Collections.Generic;

using System.Linq;

using System.Text.RegularExpressions;

using System.Web;

 

namespace HealthCalculator.Web.Controllers

{

    public class PasswordPolicy

    {

        private static int Minimum_Length = 8;

        private static int Upper_Case_length = 1;

        private static int Lower_Case_length = 1;

        private static int NonAlpha_length = 1;

        private static int Numeric_length = 1;



        public static bool IsValid(string Pwd, out string validationMsg)

        {

            if (Pwd.Length >= Minimum_Length && UpperCaseCount(Pwd) >= Upper_Case_length && LowerCaseCount(Pwd) >= Lower_Case_length && NumericCount(Pwd) >= 1 && NonAlphaCount(Pwd) >= NonAlpha_length)

            {

                validationMsg = "";

                return true;

            }



            validationMsg = "Password must be at least 8 characters, and must include at least one upper case letter, one lower case letter, one numeric digit, and one special character.";

            return false;

        }



        private static int UpperCaseCount(string Pwd)

        {

            return Regex.Matches(Pwd, "[A-Z]").Count;

        }



        private static int LowerCaseCount(string Pwd)

        {

            return Regex.Matches(Pwd, "[a-z]").Count;

        }

        private static int NumericCount(string Pwd)

        {

            return Regex.Matches(Pwd, "[0-9]").Count;

        }

        private static int NonAlphaCount(string Pwd)

        {

            return Regex.Matches(Pwd, @"[^0-9a-zA-Z\._]").Count;

        }

    }

}