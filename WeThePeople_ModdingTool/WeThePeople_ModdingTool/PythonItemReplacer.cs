﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool
{
    public class PythonItemReplacer
    {
        private IDictionary<string, string> replaceItems = new Dictionary<string, string>();
        public IDictionary<string, string> ReplaceItems
        {
            get { return replaceItems; }
            set { replaceItems = value; }
        }

        public string ReplacedString { get => replacedString; }

        string replacedString;

        public void Init()
        {
            replacedString = null;
            replaceItems.Clear();
        }

        public bool Replace(string python)
        {
            if (null == python)
            {
                return false;
            }

            if (true == ReplaceItems.Count <= 0)
            {
                return false;
            }

            StringBuilder builder = new StringBuilder(python);
            foreach (KeyValuePair<string, string> entry in ReplaceItems)
            {
                builder.Replace(entry.Key, entry.Value);
            }

            replacedString = builder.ToString();
            return true;
        }
    }
}
