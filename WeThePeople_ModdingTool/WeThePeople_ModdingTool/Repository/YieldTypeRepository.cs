using System;
using System.Collections.Generic;

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

        List<String> yieldTypeNames = new List<string>();
        public List<System.String> YieldTypeNames
        {
            get { return yieldTypeNames; }
            set { yieldTypeNames = value; }
        }

        IDictionary<string, string> yieldTypes = new Dictionary<string, string>();
        public IDictionary<string, string> YieldTypes
        {
            get { return yieldTypes; }
            set { yieldTypes = value; }
        }

        public string GetValueFromName(string name)
        {
            return yieldTypes[name];
        }
    }
}
