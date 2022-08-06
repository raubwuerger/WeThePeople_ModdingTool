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

        private string assetPathRelative = @"..\..\..\templates\Assets";

        public XmlDocument CIV4EventInfos_Start_Template;
        private string CIV4EventInfos_Start_TemplatePath = @"XML\Events\CIV4EventInfos_Start_Template.xml";
        public XmlDocument CIV4EventInfos_Done_Template;
        private string CIV4EventInfos_Done_TemplatePath = @"XML\Events\CIV4EventInfos_Done_Template.xml";

        public XmlDocument CIV4EventTriggerInfos_Start_Template;
        private string CIV4EventTriggerInfos_Start_TemplatePath = @"XML\Events\CIV4EventTriggerInfos_Start_Template.xml";
        public XmlDocument CIV4EventTriggerInfos_Done_Template;
        private string CIV4EventTriggerInfos_Done_TemplatePath = @"XML\Events\CIV4EventTriggerInfos_Done_Template.xml";

        public XmlDocument CIV4GameText_Colonization_Events_utf8_Template;
        private string CIV4GameText_Colonization_Events_utf8_TemplatePath = @"XML\Text\CIV4GameText_Colonization_Events_utf8_Template.xml";

        public string CvRandomEventInterface_Start_Template;
        private string CvRandomEventInterface_Start_TemplatePath = @"Python\EntryPoints\CvRandomEventInterface_Start_Template.py";
        public string CvRandomEventInterface_Done_Template;
        private string CvRandomEventInterface_Done_TemplatePath = @"Python\EntryPoints\CvRandomEventInterface_Done_Template.py";

        public bool Init()
        {
            return LoadTemplates();
        }
        public bool LoadTemplates()
        {
            string absoluteProgramPath = AppDomain.CurrentDomain.BaseDirectory;
            string relativeAssetPath = System.IO.Path.Combine(absoluteProgramPath, assetPathRelative);

            CIV4EventInfos_Start_Template = LoadXMLFile(System.IO.Path.Combine(relativeAssetPath,CIV4EventInfos_Start_TemplatePath));
            if ( null == CIV4EventInfos_Start_Template )
            {
                return false;
            }

            CIV4EventInfos_Done_Template = LoadXMLFile(System.IO.Path.Combine(relativeAssetPath, CIV4EventInfos_Done_TemplatePath));
            if( null == CIV4EventInfos_Done_Template )
            {
                return false;
            }

            CIV4EventTriggerInfos_Start_Template = LoadXMLFile(System.IO.Path.Combine(relativeAssetPath, CIV4EventTriggerInfos_Start_TemplatePath));
            if( null == CIV4EventTriggerInfos_Start_Template )
            {
                return false;
            }

            CIV4EventTriggerInfos_Done_Template = LoadXMLFile(System.IO.Path.Combine(relativeAssetPath, CIV4EventTriggerInfos_Done_TemplatePath));
            if( null == CIV4EventTriggerInfos_Done_Template )
            {
                return false;
            }

            CIV4GameText_Colonization_Events_utf8_Template = LoadXMLFile(System.IO.Path.Combine(relativeAssetPath, CIV4GameText_Colonization_Events_utf8_TemplatePath));
            if( null == CIV4GameText_Colonization_Events_utf8_Template )
            {
                return false;
            }

            CvRandomEventInterface_Start_Template = LoadTextFile(System.IO.Path.Combine(relativeAssetPath,CvRandomEventInterface_Start_TemplatePath));
            if ( null == CvRandomEventInterface_Start_Template )
            {
                return false;
            }

            CvRandomEventInterface_Done_Template = LoadTextFile(System.IO.Path.Combine(relativeAssetPath, CvRandomEventInterface_Done_TemplatePath));
            if( null == CvRandomEventInterface_Done_Template )
            {
                return false;
            }

            return true;
        }

        private XmlDocument LoadXMLFile(String fileName)
        {
            XMLFileLoader parser = new XMLFileLoader();
            return parser.LoadFile(fileName);
        }

        private String LoadTextFile(String fileName)
        {
            TextFileLoader textFileLoader = new TextFileLoader();
            return textFileLoader.LoadTextFile(fileName);
        }
    }
}
