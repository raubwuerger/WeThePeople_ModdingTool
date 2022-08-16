using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
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

        public string assetPathRelative = @"templates\Assets";
        public string relativeAssetPath;

        public XmlDocument CIV4EventInfos_Start_Template;
        public string CIV4EventInfos_Start_Concrete = @"XML\Events\CIV4EventInfos_Start_";

        public XmlDocument CIV4EventInfos_Done_Template;
        public string CIV4EventInfos_Done_Concrete = @"XML\Events\CIV4EventInfos_Done_";

        public XmlDocument CIV4EventTriggerInfos_Start_Template;
        public string CIV4EventTriggerInfos_Start_Concrete = @"XML\Events\CIV4EventTriggerInfos_Start_";

        public XmlDocument CIV4EventTriggerInfos_Done_Template;
        public string CIV4EventTriggerInfos_Done_Concrete = @"XML\Events\CIV4EventTriggerInfos_Done_";

        public XmlDocument CIV4GameText_Colonization_Events_utf8_Template;
        public string CIV4GameText_Colonization_Events_utf8_Concrete = @"XML\Text\CIV4GameText_Colonization_Events_utf8_Template_";

        public string CvRandomEventInterface_Start_Template;
        private string CvRandomEventInterface_Start_TemplatePath = @"Python\EntryPoints\CvRandomEventInterface_Start_Template.py";
        public string CvRandomEventInterface_Start_Concrete = @"Python\EntryPoints\CvRandomEventInterface_Start_";

        public string CvRandomEventInterface_Done_Template;
        private string CvRandomEventInterface_Done_TemplatePath = @"Python\EntryPoints\CvRandomEventInterface_Done_Template.py";
        public string CvRandomEventInterface_Done_Concrete = @"Python\EntryPoints\CvRandomEventInterface_Done_";


        public XmlDocument YieldTypesDocument;
        private string YieldTypesPath = @"templates\Civ4YieldInfos_OnlyTypes.xml";

        public XmlDocument UnitClassesDocument;
        private string UnitClassesPath = @"templates\CIV4UnitInfos_OnlyClasses.xml";

        public XmlDocument HarboursDocument;
        private string HarboursDocumentPath = @"templates\Harbours.xml";

        public bool Init()
        {
            return LoadTemplates();
        }
        //TODO: Sollte in List überführt werden. Wenn die Anzahl an templates wächst muss nur ein weiterer Eintrag in die List eingefügt werden.
        public bool LoadTemplates()
        {
            bool loadingTamplatesOk = true;
            string absoluteProgramPath = PathHelper.GetBasePath();
            relativeAssetPath = System.IO.Path.Combine(absoluteProgramPath, assetPathRelative);
            DataSetXMLFactory dataSetXMLFactory = new DataSetXMLFactory();

            DataSetXML dataSetEventInfos_Start = dataSetXMLFactory.CreateEventInfos_Start();
            TemplateRepository.Instance.RegisterTemplate(dataSetEventInfos_Start);
            CIV4EventInfos_Start_Template = dataSetEventInfos_Start.XmlDocumentObject;


            DataSetXML dataSetEventInfos_Done = dataSetXMLFactory.CreateEventInfos_Done();
            TemplateRepository.Instance.RegisterTemplate(dataSetEventInfos_Start);
            CIV4EventInfos_Done_Template = dataSetEventInfos_Done.XmlDocumentObject;

            DataSetXML dataSetEventTriggerInfos_Start = dataSetXMLFactory.CreateEventTriggerInfos_Start();
            TemplateRepository.Instance.RegisterTemplate(dataSetEventTriggerInfos_Start);
            CIV4EventTriggerInfos_Start_Template = dataSetEventTriggerInfos_Start.XmlDocumentObject;

            DataSetXML dataSetEventTriggerInfos_Done = dataSetXMLFactory.CreateEventTriggerInfos_Done();
            TemplateRepository.Instance.RegisterTemplate(dataSetEventTriggerInfos_Done);
            CIV4EventTriggerInfos_Done_Template = dataSetEventTriggerInfos_Done.XmlDocumentObject;

            DataSetXML dataSetEventGameText = dataSetXMLFactory.CreateEventGameText();
            TemplateRepository.Instance.RegisterTemplate(dataSetEventGameText);
            CIV4GameText_Colonization_Events_utf8_Template = dataSetEventGameText.XmlDocumentObject;

            CvRandomEventInterface_Start_Template = LoadTextFile(System.IO.Path.Combine(relativeAssetPath,CvRandomEventInterface_Start_TemplatePath));
            if ( null == CvRandomEventInterface_Start_Template )
            {
                loadingTamplatesOk = false;
            }

            CvRandomEventInterface_Done_Template = LoadTextFile(System.IO.Path.Combine(relativeAssetPath, CvRandomEventInterface_Done_TemplatePath));
            if( null == CvRandomEventInterface_Done_Template )
            {
                loadingTamplatesOk = false;
            }

            YieldTypesDocument = LoadXMLFile(System.IO.Path.Combine(absoluteProgramPath, YieldTypesPath));
            if (null == YieldTypesDocument)
            {
                loadingTamplatesOk = false;
            }

            if ( false == InitYieldList(YieldTypesDocument) )
            {
                loadingTamplatesOk = false;
            }

            UnitClassesDocument = LoadXMLFile(System.IO.Path.Combine(absoluteProgramPath, UnitClassesPath));
            if( null == UnitClassesDocument )
            {
                loadingTamplatesOk = false;
            }

            if( false == InitUnitClasses(UnitClassesDocument) )
            {
                loadingTamplatesOk = false;
            }

            HarboursDocument = LoadXMLFile(System.IO.Path.Combine(absoluteProgramPath, HarboursDocumentPath));
            if( null == HarboursDocument )
            {
                loadingTamplatesOk = false;
            }

            if( false == InitHarbours(HarboursDocument) )
            {
                loadingTamplatesOk = false;
            }

            return loadingTamplatesOk;
        }

        private bool RegisterDocument( string name, XmlDocument xmlDocument)
        {
            return false;
//            return TemplateRepository.Instance.RegisterTemplate(name, xmlDocument);
        }

        private bool RegisterPythonFile(string name, string pythonFile)
        {
            return TemplateRepository.Instance.RegisterTemplate(name, pythonFile);
        }

        private XmlDocument LoadXMLFile(String fileName)
        {
            XMLFileUtility parser = new XMLFileUtility();
            return parser.Load(fileName);
        }

        private String LoadTextFile(String fileName)
        {
            TextFileUtility textFileLoader = new TextFileUtility();
            return textFileLoader.Load(fileName);
        }

        private bool InitYieldList( XmlDocument yields )
        {
            List<string> yieldTypes = new List<string>();
            foreach (XmlNode node in yields.DocumentElement.ChildNodes)
            {
                yieldTypes.Add(node.InnerText);
            }
            YieldTypeRepository.Instance.YieldTypes = yieldTypes;
            return YieldTypeRepository.Instance.YieldTypes.Count > 0;
        }

        private bool InitUnitClasses( XmlDocument units )
        {
            List<string> unitClasses = new List<string>();
            foreach (XmlNode node in units.DocumentElement.ChildNodes)
            {
                unitClasses.Add(node.InnerText);
            }
            UnitClassRepository.Instance.UnitClasses = unitClasses;
            return UnitClassRepository.Instance.UnitClasses.Count > 0;
        }

        private bool InitHarbours( XmlDocument harbours )
        {
            List<string> _harbours = new List<string>();
            foreach (XmlNode node in harbours.DocumentElement.ChildNodes)
            {
                _harbours.Add(node.InnerText);
            }
            HarbourRepository.Instance.Harbours = _harbours;
            return HarbourRepository.Instance.Harbours.Count > 0;
        }
    }
}
