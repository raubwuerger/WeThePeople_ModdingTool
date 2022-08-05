using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool
{
    public sealed class MainSettingsLoader
    {
        private static readonly MainSettingsLoader instance = new MainSettingsLoader();

        static MainSettingsLoader()
        {
        }

        public static MainSettingsLoader Instance
        {
            get
            {
                return instance;
            }
        }

        private String assetPathRelative = "\\..\\..\\..\\templates\\Assets";

        public XmlDocument CIV4EventInfos_Start_Template;
        private String CIV4EventInfos_Start_TemplatePath = "\\XML\\Events\\CIV4EventInfos_Start_Template.xml";
        public XmlDocument CIV4EventInfos_Done_Template;
        private String CIV4EventInfos_Done_TemplatePath = "\\XML\\Events\\CIV4EventInfos_Done_Template.xml";

        public XmlDocument CIV4EventTriggerInfos_Start_Template;
        private String CIV4EventTriggerInfos_Start_TemplatePath = "\\XML\\Events\\CIV4EventTriggerInfos_Start_Template.xml";
        public XmlDocument CIV4EventTriggerInfos_Done_Template;
        private String CIV4EventTriggerInfos_Done_TemplatePath = "\\XML\\Events\\CIV4EventTriggerInfos_Done_Template.xml";

        public XmlDocument CIV4GameText_Colonization_Events_utf8_template;
        private String CIV4GameText_Colonization_Events_utf8_templatePath = "\\XML\\Text\\CIV4GameText_Colonization_Events_utf8_template.xml";

        public String CvRandomEventInterface_Start_Template;
        private String CvRandomEventInterface_Start_TemplatePath = "\\XML\\Python\\EntryPoints\\CvRandomEventInterface_Start_Template.py";
        public String CvRandomEventInterface_Done_Template;
        private String CvRandomEventInterface_Done_TemplatePath = "\\XML\\Python\\EntryPoints\\CvRandomEventInterface_Done_Template.py";

        public bool Init()
        {
            return LoadTemplates();
        }
        public bool LoadTemplates()
        {
            var absoluteProgramPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            CIV4EventInfos_Start_Template = LoadXMLFile(absoluteProgramPath + assetPathRelative + CIV4EventInfos_Start_TemplatePath);
            if ( null == CIV4EventInfos_Start_Template )
            {
                return false;
            }

            CIV4EventInfos_Done_Template = LoadXMLFile(absoluteProgramPath + assetPathRelative + CIV4EventInfos_Done_TemplatePath);
            if( null == CIV4EventInfos_Done_Template )
            {
                return false;
            }

            CIV4EventTriggerInfos_Start_Template = LoadXMLFile(absoluteProgramPath + assetPathRelative + CIV4EventTriggerInfos_Start_TemplatePath);
            if( null == CIV4EventTriggerInfos_Start_Template )
            {
                return false;
            }

            CIV4EventTriggerInfos_Done_Template = LoadXMLFile(absoluteProgramPath + assetPathRelative + CIV4EventTriggerInfos_Done_TemplatePath);
            if( null == CIV4EventTriggerInfos_Done_Template )
            {
                return false;
            }

            CIV4GameText_Colonization_Events_utf8_template = LoadXMLFile(absoluteProgramPath + assetPathRelative + CIV4GameText_Colonization_Events_utf8_templatePath);
            if( null == CIV4GameText_Colonization_Events_utf8_template )
            {
                return false;
            }

            return true;
        }

        private XmlDocument LoadXMLFile(String fileName)
        {
            IO.XMLFileParser parser = new IO.XMLFileParser();
            return parser.LoadFile(fileName);
        }
    }
}
