using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool
{
    public class YieldList
    {
        List<String> yields = new List<string>();
        public List<string> Yields { get => yields; set => yields = value; }


        public YieldList()
        {
            Init();
        }

        public void Init()
        {
            yields.Add("YIELD_FOOD");
            yields.Add("YIELD_LUMBER");
            yields.Add("YIELD_STONE");
            yields.Add("YIELD_ORE");
            yields.Add("YIELD_COFFEE");
            yields.Add("YIELD_WHEAT");
            yields.Add("YIELD_COCOA");
            yields.Add("YIELD_GRAPES");
            yields.Add("YIELD_FUR");
            yields.Add("YIELD_SUGAR");
            yields.Add("YIELD_WOOL");
            yields.Add("YIELD_COTTON");
        }
    }
}
