using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool
{
    public sealed class YieldList
    {
        private static readonly YieldList instance = new YieldList();

        static YieldList()
        {
        }

        public static YieldList Instance
        {
            get
            {
                return instance;
            }
        }

        List<String> yields = new List<string>();
        public List<string> Yields { get => yields; set => yields = value; }

    }
}
