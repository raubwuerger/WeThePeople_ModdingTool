using System;

namespace WeThePeople_ModdingTool.Helper
{
    public class StringHelper
    {
        public static bool IsNumberGreaterZero(string numberString)
        {
            try
            {
                int value = int.Parse(numberString);
                if (value <= 0)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        public static bool IsStringInteger(string numberString)
        {
            return int.TryParse(numberString, out int numericValue);
        }

        public static bool StringToInteger(string numberString, out int numericValue)
        {
            return int.TryParse(numberString, out numericValue);
        }

        public static string RemovePrefix(string original, string prefix)
        {
            return original.Replace(prefix, "");
        }
    }
}
