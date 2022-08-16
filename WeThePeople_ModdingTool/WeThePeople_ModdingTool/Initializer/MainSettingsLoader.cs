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
        public string CvRandomEventInterface_Start_Concrete = @"Python\EntryPoints\CvRandomEventInterface_Start_";

        public string CvRandomEventInterface_Done_Template;
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

            DataSetFactory dataSetFactory = new DataSetFactory();

            DataSetXML dataSetEventInfos_Start = dataSetFactory.CreateEventInfos_Start();
            TemplateRepository.Instance.RegisterTemplate(dataSetEventInfos_Start);
            CIV4EventInfos_Start_Template = dataSetEventInfos_Start.XmlDocumentObject;

            DataSetXML dataSetEventInfos_Done = dataSetFactory.CreateEventInfos_Done();
            TemplateRepository.Instance.RegisterTemplate(dataSetEventInfos_Done);
            CIV4EventInfos_Done_Template = dataSetEventInfos_Done.XmlDocumentObject;

            DataSetXML dataSetEventTriggerInfos_Start = dataSetFactory.CreateEventTriggerInfos_Start();
            TemplateRepository.Instance.RegisterTemplate(dataSetEventTriggerInfos_Start);
            CIV4EventTriggerInfos_Start_Template = dataSetEventTriggerInfos_Start.XmlDocumentObject;

            DataSetXML dataSetEventTriggerInfos_Done = dataSetFactory.CreateEventTriggerInfos_Done();
            TemplateRepository.Instance.RegisterTemplate(dataSetEventTriggerInfos_Done);
            CIV4EventTriggerInfos_Done_Template = dataSetEventTriggerInfos_Done.XmlDocumentObject;

            DataSetXML dataSetEventGameText = dataSetFactory.CreateEventGameText();
            TemplateRepository.Instance.RegisterTemplate(dataSetEventGameText);
            CIV4GameText_Colonization_Events_utf8_Template = dataSetEventGameText.XmlDocumentObject;

            DataSetPython dataSetRandomEventStart = dataSetFactory.CreateRandomEventStart();
            TemplateRepository.Instance.RegisterTemplate(dataSetRandomEventStart);
            CvRandomEventInterface_Start_Template = dataSetRandomEventStart.PythonContent;

            DataSetPython dataSetRandomEventDone = dataSetFactory.CreateRandomEventDone();
            TemplateRepository.Instance.RegisterTemplate(dataSetRandomEventDone);
            CvRandomEventInterface_Done_Template = dataSetRandomEventDone.PythonContent;

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

        private XmlDocument LoadXMLFile(String fileName)
        {
            XMLFileUtility parser = new XMLFileUtility();
            return parser.Load(fileName);
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
