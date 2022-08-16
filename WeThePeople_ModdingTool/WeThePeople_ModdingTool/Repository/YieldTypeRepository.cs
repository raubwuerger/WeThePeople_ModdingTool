using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool
{
    public sealed class YieldTypeRepository
    {
        private static readonly YieldTypeRepository instance = new YieldTypeRepository();

        static YieldTypeRepository()
        {
        }

        public static YieldTypeRepository Instance
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
