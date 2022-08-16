using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool
{
    public sealed class UnitClassList
    {
        private static readonly UnitClassList instance = new UnitClassList();

        static UnitClassList()
        {
        }

        public static UnitClassList Instance
        {
            get
            {
                return instance;
            }
        }

        List<String> unitClasses = new List<string>();
        public List<string> UnitClasses { get => unitClasses; set => unitClasses = value; }

    }
}
