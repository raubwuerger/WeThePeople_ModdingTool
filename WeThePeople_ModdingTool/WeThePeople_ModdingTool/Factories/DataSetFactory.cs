using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.FileUtilities;
using System.Xml;
using Serilog;

namespace WeThePeople_ModdingTool.Factories
{
    public class DataSetFactory
    {
        public static string EventTriggerInfos_Start = "EventTriggerInfos_Start";
        public static string EventTriggerInfos_Done = "EventTriggerInfos_Done";
        public static string EventGameText = "EventGameText";
        public static string RandomEvent_Start = "RandomEvent_Start";
        public static string RandomEvent_Done = "RandomEvent_Done";
        public static string EventInfos_Start = "EventInfo_Start";
        public static string EventInfos_Done_1 = "EventInfo_Done_1";
        public static string EventInfos_Done_2 = "EventInfo_Done_2";
        public static string EventInfos_Done_3 = "EventInfo_Done_3";
        public static string EventInfos_Done_4 = "EventInfo_Done_4";
        public static string EventInfos_Done_5 = "EventInfo_Done_5";

        public static string NODE_EVENTS = "Events";
        public static string NODE_EVENT = "Event";
        public static string NODE_TYPE = "Type";

        private static string FileExtensionXML = ".xml";
        private static string FileExtensionPython = ".py";
        private static string RootNode_EventInfo = "/Civ4EventInfos";
        public static string ConcreteNode_EventInfo = "EventInfo";
        private static string RootNode_EventTriggerInfo = "/Civ4EventTriggerInfos";
        public static string ConcreteNode_EventTriggerInfo = "EventTriggerInfo";
        private static string RootNode_Civ4GameText = "/Civ4GameText";
        public static string ConcreteNode_Civ4GameText = "Civ4GameText";

        public DataSetPython CreateRandomEventStart()
        {
            DataSetPython dataSet = CreateBasePython(RandomEvent_Start);
            dataSet.TemplateNameWithoutExtension = "CvRandomEventInterface";
            dataSet.TemplateNameCIV4 = dataSet.TemplateNameWithoutExtension + dataSet.TemplateFileExtension;
            dataSet.TemplateFileNameRelativ = @"Python\EntryPoints\CvRandomEventInterface_Start_Template.py";
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.TemplateFileNameProcessed = @"Python\EntryPoints\CvRandomEventInterface_Start_";
            dataSet.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameProcessed);
            dataSet.BaseAssetPath = @"Assets\Python\EntryPoints";
            dataSet.PythonContentTemplate = TextFileUtility.LoadFileText(dataSet);
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            return dataSet;
        }
        public DataSetPython CreateRandomEventDone()
        {
            DataSetPython dataSet = CreateBasePython(RandomEvent_Done);
            dataSet.TemplateNameWithoutExtension = "CvRandomEventInterface";
            dataSet.TemplateNameCIV4 = dataSet.TemplateNameWithoutExtension + dataSet.TemplateFileExtension;
            dataSet.TemplateFileNameRelativ = @"Python\EntryPoints\CvRandomEventInterface_Done_Template.py";
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.TemplateFileNameProcessed = @"Python\EntryPoints\CvRandomEventInterface_Done_";
            dataSet.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameProcessed);
            dataSet.BaseAssetPath = @"Assets\Python\EntryPoints";
            dataSet.PythonContentTemplate = TextFileUtility.LoadFileText(dataSet);
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            return dataSet;
        }
        public DataSetXML CreateEventTriggerInfos_Start()
        {
            DataSetXML dataSet = CreateBaseXML(EventTriggerInfos_Start);
            dataSet.TemplateNameWithoutExtension = "CIV4EventTriggerInfos";
            dataSet.TemplateNameCIV4 = dataSet.TemplateNameWithoutExtension + dataSet.TemplateFileExtension;
            dataSet.TemplateFileNameRelativ = @"XML\Events\CIV4EventTriggerInfos_Start_Template.xml";
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.TemplateFileNameProcessed = @"XML\Events\CIV4EventTriggerInfos_Start_";
            dataSet.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameProcessed);
            dataSet.BaseAssetPath = @"Assets\XML\Events";
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
            DataSetXML dataSet = CreateBaseXML(EventTriggerInfos_Done);
            dataSet.TemplateNameWithoutExtension = "CIV4EventTriggerInfos";
            dataSet.TemplateNameCIV4 = dataSet.TemplateNameWithoutExtension + dataSet.TemplateFileExtension;
            dataSet.TemplateFileNameRelativ = @"XML\Events\CIV4EventTriggerInfos_Done_Template.xml";
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.TemplateFileNameProcessed = @"XML\Events\CIV4EventTriggerInfos_Done_";
            dataSet.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameProcessed);
            dataSet.BaseAssetPath = @"Assets\XML\Events";
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
            DataSetXML dataSet = CreateBaseXML(EventGameText);
            dataSet.TemplateNameWithoutExtension = "CIV4GameText_Colonization_Events_utf8";
            dataSet.TemplateNameCIV4 = dataSet.TemplateNameWithoutExtension + dataSet.TemplateFileExtension;
            dataSet.TemplateFileNameRelativ = @"XML\Text\CIV4GameText_Colonization_Events_utf8_Template.xml";
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.TemplateFileNameProcessed = @"XML\Text\CIV4GameText_Colonization_Events_utf8_Template_";
            dataSet.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameProcessed);
            dataSet.BaseAssetPath = @"Assets\XML\Text";
            dataSet.XmlRootNode = RootNode_Civ4GameText;
            dataSet.XmlConcreteNode = ConcreteNode_Civ4GameText;
            dataSet.XmlDocumentTemplate = XMLFileUtility.LoadFileXML(dataSet);
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            dataSet.TemplateReplaceItems.Add(ReplaceItems.DONE_INDEX, "1");
            return dataSet;
        }

        public DataSetXML CreateEventInfos_Start()
        {
            DataSetXML dataSet = CreateBaseXML(EventInfos_Start);
            dataSet.TemplateNameWithoutExtension = "CIV4EventInfos";
            dataSet.TemplateNameCIV4 = dataSet.TemplateNameWithoutExtension + dataSet.TemplateFileExtension;
            dataSet.TemplateFileNameRelativ = @"XML\Events\CIV4EventInfos_Start_Template.xml";
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.TemplateFileNameProcessed = @"XML\Events\CIV4EventInfos_Start_";
            dataSet.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameProcessed);
            dataSet.BaseAssetPath = @"Assets\XML\Events"; 
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
            dataSet.TemplateNameWithoutExtension = "CIV4EventInfos";
            dataSet.TemplateNameCIV4 = dataSet.TemplateNameWithoutExtension + dataSet.TemplateFileExtension;
            dataSet.TemplateFileNameRelativ = @"XML\Events\CIV4EventInfos_Done_Template.xml";
            dataSet.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameRelativ);
            dataSet.TemplateFileNameProcessed = @"XML\Events\CIV4EventInfos_Done_";
            dataSet.BaseAssetPath = @"Assets\XML\Events";
            dataSet.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSet.TemplateFileNameProcessed);
            dataSet.XmlRootNode = RootNode_EventInfo;
            dataSet.XmlConcreteNode = ConcreteNode_EventInfo;
            dataSet.XmlDocumentTemplate = XMLFileUtility.LoadFileXML(dataSet);

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

        private string CreateNameEventInfoDone()
        {
            IDictionary<string, DataSetXML> registered = TemplateRepository.Instance.XmlDocumentEventDone;
            if( registered.Count == 0 )
            {
                return EventInfos_Done_1;
            }
            else if ( registered.Count == 1 )
            {
                return EventInfos_Done_2;
            }
            else if (registered.Count == 2)
            {
                return EventInfos_Done_3;
            }
            else if (registered.Count == 3)
            {
                return EventInfos_Done_4;
            }
            else if (registered.Count == 4)
            {
                return EventInfos_Done_5;
            }
            return String.Empty;
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
            dataSetXML.TemplateFileExtension = FileExtensionXML;
            dataSetXML.TemplateFileNameAndPathProcessed = "";
            return dataSetXML;
        }

        private DataSetPython CreateBasePython(string templateName)
        {
            DataSetPython dataSetPython = new DataSetPython();
            dataSetPython.TemplateName = templateName;
            dataSetPython.TemplateFileExtension = FileExtensionPython;
            dataSetPython.TemplateFileNameAndPathProcessed = "";
            return dataSetPython;
        }

    }
}
