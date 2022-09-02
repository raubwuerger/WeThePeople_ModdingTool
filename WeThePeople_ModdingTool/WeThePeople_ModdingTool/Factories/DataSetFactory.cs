using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.FileUtilities;
using System.Xml;
using Serilog;
using System.IO;

namespace WeThePeople_ModdingTool.Factories
{
    public class DataSetFactory
    {
        //Template names
        public static readonly string CvRandomEventInterface_Done = "CvRandomEventInterface_Done.py";
        public static readonly string CvRandomEventInterface_Start = "CvRandomEventInterface_Start.py";
        public static readonly string CIV4EventInfos_Done = "CIV4EventInfos_Done.xml";
        public static readonly string CIV4EventInfos_Start = "CIV4EventInfos_Start.xml";
        public static readonly string CIV4EventTriggerInfos_Done = "CIV4EventTriggerInfos_Done.xml";
        public static readonly string CIV4EventTriggerInfos_Start = "CIV4EventTriggerInfos_Start.xml";
        public static readonly string CIV4GameText_Colonization_Events_utf8_Start = "CIV4GameText_Colonization_Events_utf8_Start.xml";
        public static readonly string CIV4GameText_Colonization_Events_utf8_Done = "CIV4GameText_Colonization_Events_utf8_Done.xml";

        private static string CvRandomEventInterface_PathRelative = @"Python\EntryPoints\";
        private static string CIV4EventInfos_PathRelative = @"XML\Events\";
        private static string CIV4EventTriggerInfos_PathRelative = @"XML\Events\";
        private static string CIV4GameText_Colonization_Events_utf8_PathRelative = @"XML\Text\";

        //Original names
        private static string CvRandomEventInterface = "CvRandomEventInterface.py";
        private static string CIV4EventInfos = "CIV4EventInfos.xml";
        private static string CIV4EventTriggerInfos = "CIV4EventTriggerInfos.xml";
        private static string CIV4GameText_Colonization_Events_utf8 = "CIV4GameText_Colonization_Events_utf8.xml";

        public static string EventInfos_Done_1 = "CIV4EventInfos_Done_1.xml";
        public static string EventInfos_Done_2 = "CIV4EventInfos_Done_2.xml";
        public static string EventInfos_Done_3 = "CIV4EventInfos_Done_3.xml";
        public static string EventInfos_Done_4 = "CIV4EventInfos_Done_4.xml";
        public static string EventInfos_Done_5 = "CIV4EventInfos_Done_5.xml";
        public static string EventInfos_Done_6 = "CIV4EventInfos_Done_6.xml";

        public static string GameText_Done_1 = "GameText_Done_1";

        public static string NODE_EVENTS = "Events";
        public static string NODE_EVENT = "Event";
        public static string NODE_TYPE = "Type";

        public static string NODE_TAG = "Tag";
        public static string NODE_TEXT = "TEXT";
        public static string NODE_DESCRIPTION = "Description";

        private static string RootNode_EventInfo = "/Civ4EventInfos";
        public static string ConcreteNode_EventInfo = "EventInfo";
        private static string RootNode_EventTriggerInfo = "/Civ4EventTriggerInfos";
        public static string ConcreteNode_EventTriggerInfo = "EventTriggerInfo";
        private static string RootNode_Civ4GameText = "/Civ4GameText";
        public static string ConcreteNode_Civ4GameText = "Civ4GameText";


        public DataSetPython CreateRandomEventStart()
        {
            DataSetPython dataSet = CreateBasePython(CvRandomEventInterface_Start);
            dataSet.OriginalFileName = CvRandomEventInterface;
            dataSet.TemplateFileNameRelativ = PathHelper.PathCombine(CvRandomEventInterface_PathRelative, dataSet.TemplateName);
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.PythonContentTemplate = TextFileUtility.LoadFileText(dataSet);
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            return dataSet;
        }
        public DataSetPython CreateRandomEventDone()
        {
            DataSetPython dataSet = CreateBasePython(CvRandomEventInterface_Done);
            dataSet.OriginalFileName = CvRandomEventInterface;
            dataSet.TemplateFileNameRelativ = PathHelper.PathCombine(CvRandomEventInterface_PathRelative, dataSet.TemplateName);
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.PythonContentTemplate = TextFileUtility.LoadFileText(dataSet);
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            return dataSet;
        }
        public DataSetXML CreateEventTriggerInfos_Start()
        {
            DataSetXML dataSet = CreateBaseXML(CIV4EventTriggerInfos_Start);
            dataSet.OriginalFileName = CIV4EventTriggerInfos;
            dataSet.TemplateFileNameRelativ = PathHelper.PathCombine(CIV4EventTriggerInfos_PathRelative, dataSet.TemplateName);
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.XmlRootNode = RootNode_EventTriggerInfo;
            dataSet.XmlConcreteNode = ConcreteNode_EventTriggerInfo;
            dataSet.XmlDocumentTemplate = XMLFileUtility.LoadFileXML(dataSet);
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");

            return dataSet;
        }
        public DataSetXML CreateEventTriggerInfos_Done()
        {
            DataSetXML dataSet = CreateBaseXML(CIV4EventTriggerInfos_Done);
            dataSet.OriginalFileName = CIV4EventTriggerInfos;
            dataSet.TemplateFileNameRelativ = PathHelper.PathCombine(CIV4EventTriggerInfos_PathRelative, dataSet.TemplateName);
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.XmlRootNode = RootNode_EventTriggerInfo;
            dataSet.XmlConcreteNode = ConcreteNode_EventTriggerInfo;
            dataSet.XmlDocumentTemplate = XMLFileUtility.LoadFileXML(dataSet);
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");

            return dataSet;
        }

        public DataSetXML CreateEventGameText()
        {
            DataSetXML dataSet = CreateBaseXML(CIV4GameText_Colonization_Events_utf8_Start);
            dataSet.OriginalFileName = CIV4GameText_Colonization_Events_utf8;
            dataSet.TemplateFileNameRelativ = PathHelper.PathCombine(CIV4GameText_Colonization_Events_utf8_PathRelative, dataSet.TemplateName);
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.XmlRootNode = RootNode_Civ4GameText;
            dataSet.XmlConcreteNode = ConcreteNode_Civ4GameText;
            dataSet.XmlDocumentTemplate = XMLFileUtility.LoadFileXML(dataSet);
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            return dataSet;
        }

        public DataSetXML CreateEventGameTextDone( string name )
        {
            DataSetXML dataSet = CreateBaseXML(name);
            dataSet.OriginalFileName = CIV4EventInfos;
            dataSet.TemplateFileNameRelativ = PathHelper.PathCombine(CIV4GameText_Colonization_Events_utf8_PathRelative, dataSet.TemplateName);
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.XmlRootNode = RootNode_Civ4GameText;
            dataSet.XmlConcreteNode = ConcreteNode_Civ4GameText;
            dataSet.XmlDocumentTemplate = XMLFileUtility.Load(CreateTemplateFileNameAndPathAbsolute_EventGameTextDone());
            return dataSet;
        }
        private string CreateTemplateFileNameAndPathAbsolute_EventGameTextDone()
        {
            string templateFileName = PathHelper.PathCombine(CIV4GameText_Colonization_Events_utf8_PathRelative, CIV4GameText_Colonization_Events_utf8_Done);
            return PathHelper.GetFullAssetFileName(templateFileName);
        }


        public DataSetXML CreateEventInfos_Start()
        {
            DataSetXML dataSet = CreateBaseXML(CIV4EventInfos_Start);
            dataSet.OriginalFileName = CIV4EventInfos;
            dataSet.TemplateFileNameRelativ = PathHelper.PathCombine(CIV4EventInfos_PathRelative, dataSet.TemplateName);
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.XmlRootNode = RootNode_EventInfo;
            dataSet.XmlConcreteNode = ConcreteNode_EventInfo;
            dataSet.XmlDocumentTemplate = XMLFileUtility.LoadFileXML(dataSet);
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.TRIGGER_VALUE_START, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.TRIGGER_VALUE_DONE, "");
            return dataSet;
        }

        public DataSetXML CreateEventInfo_Done()
        {
            DataSetXML dataSet = CreateBaseXML(CreateNameEventInfoDone());
            dataSet.OriginalFileName = CIV4EventInfos;
            dataSet.TemplateFileNameRelativ = PathHelper.PathCombine(CIV4EventInfos_PathRelative, dataSet.TemplateName);
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.XmlRootNode = RootNode_EventInfo;
            dataSet.XmlConcreteNode = ConcreteNode_EventInfo;
            dataSet.XmlDocumentTemplate = XMLFileUtility.Load(CreateTemplateFileNameAndPathAbsolute_EventInfoDone());

            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.GOLD, "0");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.UNIT_CLASS, "UNITCLASS_NONE");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.UNIT_COUNT, "0");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.UNIT_EXPERIENCE, "0");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.KING_RELATION, "0");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD_PRICE, "0");

            return dataSet;
        }

        private string CreateTemplateFileNameAndPathAbsolute_EventInfoDone()
        {
            string templateFileName = PathHelper.PathCombine(CIV4EventInfos_PathRelative, CIV4EventInfos_Done);
            return PathHelper.GetFullAssetFileName(templateFileName);
        }

        private string CreateNameEventInfoDone()
        {
            IDictionary<string, DataSetXML> registered = TemplateRepository.Instance.XmlDocumentEventDone;
            List<string> eventInfoDons = CreateEventInfoDoneList();

            foreach(string eventInfo in eventInfoDons)
            {
                if( false == registered.ContainsKey(eventInfo) )
                {
                    return eventInfo;
                }
            }
            return String.Empty;
        }

        private List<string> CreateEventInfoDoneList()
        {
            List<string> list = new List<string>();
            list.Add(EventInfos_Done_1);
            list.Add(EventInfos_Done_2);
            list.Add(EventInfos_Done_3);
            list.Add(EventInfos_Done_4);
            list.Add(EventInfos_Done_5);
            list.Add(EventInfos_Done_6);
            return list;
        }

        private bool FindStringInList( string toFind, IDictionary<string, DataSetXML> source )
        {
            foreach (KeyValuePair<string, DataSetXML> entry in source)
            {
                if( entry.Equals(toFind) )
                {
                    return true;
                }
            }
            return false;
        }


        private DataSetXML CreateBaseXML( string templateName )
        {
            DataSetXML dataSetXML = new DataSetXML();
            dataSetXML.TemplateName = templateName;
            dataSetXML.TemplateFileExtension = Path.GetExtension(templateName);
            return dataSetXML;
        }

        private DataSetPython CreateBasePython(string templateName)
        {
            DataSetPython dataSetPython = new DataSetPython();
            dataSetPython.TemplateName = templateName;
            dataSetPython.TemplateFileExtension = Path.GetExtension(templateName);
            return dataSetPython;
        }

    }
}
