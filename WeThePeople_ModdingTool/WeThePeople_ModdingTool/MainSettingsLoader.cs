using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.FileUtilities;

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
        private String CvRandomEventInterface_Start_TemplatePathAbsolute = "D:\\C_sharp\\WeThePeople_ModdingTool\\WeThePeople_ModdingTool\\WeThePeople_ModdingTool\\templates\\Assets\\Python\\EntryPoints\\CvRandomEventInterface_Start_Template.py";
        public String CvRandomEventInterface_Done_Template;
        private String CvRandomEventInterface_Done_TemplatePathAbsolute = "D:\\C_sharp\\WeThePeople_ModdingTool\\WeThePeople_ModdingTool\\WeThePeople_ModdingTool\\templates\\Assets\\Python\\EntryPoints\\CvRandomEventInterface_Done_Template.py";

        public bool Init()
        {
            return LoadTemplates();
        }
        public bool LoadTemplates()
        {
            var absoluteProgramPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            var relativeAssetPath = absoluteProgramPath + assetPathRelative;

            CIV4EventInfos_Start_Template = LoadXMLFile(relativeAssetPath + CIV4EventInfos_Start_TemplatePath);
            if ( null == CIV4EventInfos_Start_Template )
            {
                return false;
            }

            CIV4EventInfos_Done_Template = LoadXMLFile(relativeAssetPath + CIV4EventInfos_Done_TemplatePath);
            if( null == CIV4EventInfos_Done_Template )
            {
                return false;
            }

            CIV4EventTriggerInfos_Start_Template = LoadXMLFile(relativeAssetPath + CIV4EventTriggerInfos_Start_TemplatePath);
            if( null == CIV4EventTriggerInfos_Start_Template )
            {
                return false;
            }

            CIV4EventTriggerInfos_Done_Template = LoadXMLFile(relativeAssetPath + CIV4EventTriggerInfos_Done_TemplatePath);
            if( null == CIV4EventTriggerInfos_Done_Template )
            {
                return false;
            }

            CIV4GameText_Colonization_Events_utf8_template = LoadXMLFile(relativeAssetPath + CIV4GameText_Colonization_Events_utf8_templatePath);
            if( null == CIV4GameText_Colonization_Events_utf8_template )
            {
                return false;
            }

            CvRandomEventInterface_Start_Template = LoadTextFile(CvRandomEventInterface_Start_TemplatePathAbsolute);
            if ( null == CvRandomEventInterface_Start_Template )
            {
                return false;
            }

            CvRandomEventInterface_Done_Template = LoadTextFile(CvRandomEventInterface_Done_TemplatePathAbsolute);
            if( null == CvRandomEventInterface_Done_Template )
            {
                return false;
            }

            return true;
        }

        private XmlDocument LoadXMLFile(String fileName)
        {
            IO.XMLFileLoader parser = new IO.XMLFileLoader();
            return parser.LoadFile(fileName);
        }

        private String LoadTextFile(String fileName)
        {
            TextFileLoader textFileLoader = new TextFileLoader();
            return textFileLoader.LoadTextFile(fileName);
        }
    }
}
