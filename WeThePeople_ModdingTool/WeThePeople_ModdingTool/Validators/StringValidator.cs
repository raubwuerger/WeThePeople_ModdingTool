using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool.Validators
{
    public class StringValidator
    {
        public static bool IsNull(string rhs)
        {
            return rhs == null;
        }

        public static bool IsNullOrEmpty( string rhs )
        {
            return string.IsNullOrEmpty(rhs);
        }

        public static bool IsNullOrWhiteSpace( string rhs )
        {
            return string.IsNullOrWhiteSpace(rhs);
        }

        public static bool IsNumeric( string rhs )
        {
            double isDouble;
            return double.TryParse(rhs, out isDouble);
        }

        public static bool GetNaturalNumber( string rhs, out int number )
        {
            return int.TryParse(rhs, out number);
        }
    }
}
