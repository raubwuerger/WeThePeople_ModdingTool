using System;
using System.Collections.Generic;
using System.Text;

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

        private string mod_path;
        public string Mod_path
        {
            get { return mod_path; }
            set { mod_path = value; }
        }
    }
}
