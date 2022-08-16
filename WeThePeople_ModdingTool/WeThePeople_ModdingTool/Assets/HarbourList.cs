using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool.Assets
{
    public sealed class HarbourList
    {
        private static readonly HarbourList instance = new HarbourList();

        static HarbourList()
        {
        }

        public static HarbourList Instance
        {
            get
            {
                return instance;
            }
        }

        List<String> harbours = new List<string>();
        public List<string> Harbours { get => harbours; set => harbours = value; }
    }
}
