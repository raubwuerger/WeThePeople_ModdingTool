using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool
{
    public sealed class YieldTypeList
    {
        private static readonly YieldTypeList instance = new YieldTypeList();

        static YieldTypeList()
        {
        }

        public static YieldTypeList Instance
        {
            get
            {
                return instance;
            }
        }

        List<String> yieldTypes = new List<string>();
        public List<string> YieldTypes { get => yieldTypes; set => yieldTypes = value; }

    }
}
