using System;
using System.Collections.Generic;
using System.Text;

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

        List<String> unitClasses = new List<string>();
        public List<string> UnitClasses { get => unitClasses; set => unitClasses = value; }

    }
}
