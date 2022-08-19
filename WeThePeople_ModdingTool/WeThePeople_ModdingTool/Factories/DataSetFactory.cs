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
        public static string EventInfos_Start = "EventInfos_Start";
        public static string EventInfos_Done_1 = "EventInfos_Done_1";
        public static string EventInfos_Done_2 = "EventInfos_Done_2";
        public static string EventInfos_Done_3 = "EventInfos_Done_3";
        public static string EventInfos_Done_4 = "EventInfos_Done_4";
        public static string EventInfos_Done_5 = "EventInfos_Done_5";

        private static string FileExtensionXML = ".xml";
        private static string FileExtensionPython = ".py";
        private static string RootNode_EventInfo = "/EventInfo";
        private static string RootNode_EventTriggerInfo = "/EventTriggerInfo";
        private static string RootNode_Civ4GameText = "/Civ4GameText";

        public DataSetXML CreateEventInfos_Start()
        {
            DataSetXML dataSetXML = CreateBaseXML(EventInfos_Start);
            dataSetXML.TemplateFileNameRelativ = @"XML\Events\CIV4EventInfos_Start_Template.xml";
            dataSetXML.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameRelativ);
            dataSetXML.TemplateFileNameProcessed = @"XML\Events\CIV4EventInfos_Start_";
            dataSetXML.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameProcessed);
            dataSetXML.XmlRootNode = RootNode_EventInfo;
            dataSetXML.XmlDocumentTemplate = LoadFileXML(dataSetXML);
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.TRIGGER_VALUE_START, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.TRIGGER_VALUE_DONE, "");
            return dataSetXML;
        }

        public DataSetXML CreateEventInfo_Done()
        {
            DataSetXML dataSetXML = CreateBaseXML(CreateNameEventInfoDone());
            dataSetXML.TemplateFileNameRelativ = @"XML\Events\CIV4EventInfos_Done_Template.xml";
            dataSetXML.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameRelativ);
            dataSetXML.TemplateFileNameProcessed = @"XML\Events\CIV4EventInfos_Done_";
            dataSetXML.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameProcessed);
            dataSetXML.XmlRootNode = RootNode_EventInfo;
            dataSetXML.XmlDocumentTemplate = LoadFileXML(dataSetXML);

            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.GOLD, "0");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.UNIT_CLASS, "UNITCLASS_NONE");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.UNIT_COUNT, "0");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.UNIT_EXPERIENCE, "0");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.KING_RELATION, "0");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.YIELD_PRICE, "0");

            return dataSetXML;
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

        public DataSetXML CreateEventTriggerInfos_Start()
        {
            DataSetXML dataSetXML = CreateBaseXML(EventTriggerInfos_Start);
            dataSetXML.TemplateFileNameRelativ = @"XML\Events\CIV4EventTriggerInfos_Start_Template.xml";
            dataSetXML.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameRelativ);
            dataSetXML.TemplateFileNameProcessed = @"XML\Events\CIV4EventTriggerInfos_Start_";
            dataSetXML.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameProcessed);
            dataSetXML.XmlRootNode = RootNode_EventTriggerInfo;
            dataSetXML.XmlDocumentTemplate = LoadFileXML(dataSetXML);
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");

            return dataSetXML;
        }
        public DataSetXML CreateEventTriggerInfos_Done()
        {
            DataSetXML dataSetXML = CreateBaseXML(EventTriggerInfos_Done);
            dataSetXML.TemplateFileNameRelativ = @"XML\Events\CIV4EventTriggerInfos_Done_Template.xml";
            dataSetXML.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameRelativ);
            dataSetXML.TemplateFileNameProcessed = @"XML\Events\CIV4EventTriggerInfos_Done_";
            dataSetXML.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameProcessed);
            dataSetXML.XmlRootNode = RootNode_EventTriggerInfo;
            dataSetXML.XmlDocumentTemplate = LoadFileXML(dataSetXML);
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");

            return dataSetXML;
        }

        public DataSetXML CreateEventGameText()
        {
            DataSetXML dataSetXML = CreateBaseXML(EventGameText);
            dataSetXML.TemplateFileNameRelativ = @"XML\Text\CIV4GameText_Colonization_Events_utf8_Template.xml";
            dataSetXML.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameRelativ);
            dataSetXML.TemplateFileNameProcessed = @"XML\Text\CIV4GameText_Colonization_Events_utf8_Template_";
            dataSetXML.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameProcessed);
            dataSetXML.XmlRootNode = RootNode_Civ4GameText;
            dataSetXML.XmlDocumentTemplate = LoadFileXML(dataSetXML);
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSetXML.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            return dataSetXML;
        }

        public DataSetPython CreateRandomEventStart()
        {
            DataSetPython dataSetPython = CreateBasePython(RandomEvent_Start);
            dataSetPython.TemplateFileNameRelativ = @"Python\EntryPoints\CvRandomEventInterface_Start_Template.py";
            dataSetPython.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSetPython.TemplateFileNameRelativ);
            dataSetPython.TemplateFileNameProcessed = @"Python\EntryPoints\CvRandomEventInterface_Start_";
            dataSetPython.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSetPython.TemplateFileNameProcessed);
            dataSetPython.PythonContentTemplate = LoadFileText(dataSetPython);
            dataSetPython.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSetPython.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSetPython.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            return dataSetPython;
        }
        public DataSetPython CreateRandomEventDone()
        {
            DataSetPython dataSetPython = CreateBasePython(RandomEvent_Done);
            dataSetPython.TemplateFileNameRelativ = @"Python\EntryPoints\CvRandomEventInterface_Done_Template.py";
            dataSetPython.TemplateFileNameAndPathAbsolute = PathHelper.GetFullAssetFileName(dataSetPython.TemplateFileNameRelativ);
            dataSetPython.TemplateFileNameProcessed = @"Python\EntryPoints\CvRandomEventInterface_Done_";
            dataSetPython.TemplateFileNameAndPathProcessed = PathHelper.GetFullAssetFileName(dataSetPython.TemplateFileNameProcessed);
            dataSetPython.PythonContentTemplate = LoadFileText(dataSetPython);
            dataSetPython.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSetPython.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSetPython.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
            return dataSetPython;
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

        private XmlDocument LoadFileXML( DataSetBase dataSetBase )
        {
            XMLFileUtility fileUtility = new XMLFileUtility();
            return fileUtility.Load(dataSetBase.TemplateFileNameAndPathAbsolute);
        }

        private String LoadFileText( DataSetBase dataSetBase )
        {
            TextFileUtility textFileUtility = new TextFileUtility();
            return textFileUtility.Load( dataSetBase.TemplateFileNameAndPathAbsolute );
        }

    }
}
