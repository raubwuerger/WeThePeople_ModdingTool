﻿using System.Collections.Generic;

namespace WeThePeople_ModdingTool.Helper
{
    public class DictionaryHelper
    {
        public static List<string> GetKeys(IDictionary<string, string> dictionary)
        {
            return new List<string>(dictionary.Keys);
        }

        public static List<string> GetValues(IDictionary<string, string> dictionary)
        {
            return new List<string>(dictionary.Values);
        }
    }
}
