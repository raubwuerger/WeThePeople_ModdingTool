using System;
using System.Collections.Generic;

namespace WeThePeople_ModdingTool
{
    public sealed class UnitClassRepository
    {
        private static readonly UnitClassRepository instance = new UnitClassRepository();

        static UnitClassRepository()
        {
        }

        public static UnitClassRepository Instance
        {
            get
            {
                return instance;
            }
        }

        List<String> unitClassNames = new List<string>();
        public List<string> UnitClassNames
        {
            get => unitClassNames;
            set => unitClassNames = value;
        }

        IDictionary<string, string> unitClasses = new Dictionary<string, string>();
        public IDictionary<string, string> UnitClasses
        {
            get { return unitClasses; }
            set { unitClasses = value; }
        }

        public string GetValueFromName(string name)
        {
            return unitClasses[name];
        }

        public string GetKeyFromValue(string value)
        {
            foreach (string key in unitClasses.Keys)
            {
                if (unitClasses[key] == value)
                {
                    return key;
                }
            }
            return null;
        }
    }
}
