using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.FileUtilities;

namespace WeThePeople_ModdingTool
{
    public class WeThePeople_ModdingTool_Config
    {
        private static readonly WeThePeople_ModdingTool_Config instance = new WeThePeople_ModdingTool_Config();

        static WeThePeople_ModdingTool_Config()
        {
        }

        public static WeThePeople_ModdingTool_Config Instance
        {
            get
            {
                return instance;
            }
        }

        public static readonly string MOD_UNIT_PATH = @"Assets\XML\Units";
        public static readonly string CIV4UnitInfos = "CIV4UnitInfos.xml";

        private string mod_path;
        public string Mod_path
        {
            get { return mod_path; }
            set { mod_path = value; }
        }

        public string GetFullPathCIV4UnitInfos()
        {
            return PathHelper.CombinePathAndFileName(PathHelper.PathCombine(mod_path, MOD_UNIT_PATH), CIV4UnitInfos);
        }
    }
}
