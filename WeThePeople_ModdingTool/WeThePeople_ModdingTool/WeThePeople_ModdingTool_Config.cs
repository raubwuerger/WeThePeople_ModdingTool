using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.FileUtilities;
using Serilog;

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

        private readonly string PATH_RELATIVE = @"program";
        public readonly string MAIN_CONFIG_FILE = "WeThePeople_ModdingTool.xml";
        public readonly string MOD_SOURCE_PATH = "ModSourcePath";
        public readonly string MOD_SOURCE_PATH_PATH = "Path";
        public readonly string MOD_UNIT_PATH = @"Assets\XML\Units";
        public readonly string CIV4UnitInfos = "CIV4UnitInfos.xml";

        public string GetBasePathAndFileName()
        {
            return PathHelper.CombinePathAndFileName(PathHelper.CombinePath(PathHelper.GetBasePath(), PATH_RELATIVE), MAIN_CONFIG_FILE);
        }


        private string mod_path;
        public string Mod_path
        {
            get { return mod_path; }
            set { mod_path = value; }
        }

        public string GetFullPathCIV4UnitInfos()
        {
            return PathHelper.CombinePathAndFileName(PathHelper.CombinePath(mod_path, MOD_UNIT_PATH), CIV4UnitInfos);
        }
    }
}
