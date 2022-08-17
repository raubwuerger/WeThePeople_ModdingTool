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
        public static string EventInfos_Start = "EventInfos_Start";
        public static string EventInfos_Done = "EventInfos_Done";
        public static string EventTriggerInfos_Start = "EventTriggerInfos_Start";
        public static string EventTriggerInfos_Done = "EventTriggerInfos_Done";
        public static string EventGameText = "EventGameText";
        public static string RandomEvent_Start = "RandomEvent_Start";
        public static string RandomEvent_Done = "RandomEvent_Done";

        private static string FileExtensionXML = ".xml";
        private static string FileExtensionPython = ".py";
        private static string RootNodeEventInfo = "/EventInfo";
        private static string RootNodeEventTriggerInfo = "/EventTriggerInfo";
        public DataSetXML CreateEventInfos_Start()
        {
            DataSetXML dataSetXML = CreateBaseXML(EventInfos_Start);
            dataSetXML.TemplateFileNameRelativ = @"XML\Events\CIV4EventInfos_Start_Template.xml";
            dataSetXML.TemplateFileNameAbsolute = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameRelativ);
            dataSetXML.TemplateFileNameConcrete = PathHelper.GetFullAssetFileName(@"XML\Events\CIV4EventInfos_Start_");
            dataSetXML.XmlRootNode = RootNodeEventInfo;
            dataSetXML.XmlDocumentTemplate = LoadFileXML(dataSetXML);
            return dataSetXML;
        }

        public DataSetXML CreateEventInfos_Done()
        {
            DataSetXML dataSetXML = CreateBaseXML(EventInfos_Done);
            dataSetXML.TemplateFileNameRelativ = @"XML\Events\CIV4EventInfos_Done_Template.xml";
            dataSetXML.TemplateFileNameAbsolute = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameRelativ);
            dataSetXML.TemplateFileNameConcrete = PathHelper.GetFullAssetFileName(@"XML\Events\CIV4EventInfos_Done_");
            dataSetXML.XmlRootNode = RootNodeEventInfo;
            dataSetXML.XmlDocumentTemplate = LoadFileXML(dataSetXML);
            return dataSetXML;
        }

        public DataSetXML CreateEventTriggerInfos_Start()
        {
            DataSetXML dataSetXML = CreateBaseXML(EventTriggerInfos_Start);
            dataSetXML.TemplateFileNameRelativ = @"XML\Events\CIV4EventTriggerInfos_Start_Template.xml";
            dataSetXML.TemplateFileNameAbsolute = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameRelativ);
            dataSetXML.TemplateFileNameConcrete = PathHelper.GetFullAssetFileName(@"XML\Events\CIV4EventTriggerInfos_Start_");
            dataSetXML.XmlRootNode = RootNodeEventTriggerInfo;
            dataSetXML.XmlDocumentTemplate = LoadFileXML(dataSetXML);
            return dataSetXML;
        }
        public DataSetXML CreateEventTriggerInfos_Done()
        {
            DataSetXML dataSetXML = CreateBaseXML(EventTriggerInfos_Done);
            dataSetXML.TemplateFileNameRelativ = @"XML\Events\CIV4EventTriggerInfos_Done_Template.xml";
            dataSetXML.TemplateFileNameAbsolute = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameRelativ);
            dataSetXML.TemplateFileNameConcrete = PathHelper.GetFullAssetFileName(@"XML\Events\CIV4EventTriggerInfos_Done_");
            dataSetXML.XmlRootNode = RootNodeEventTriggerInfo;
            dataSetXML.XmlDocumentTemplate = LoadFileXML(dataSetXML);
            return dataSetXML;
        }

        public DataSetXML CreateEventGameText()
        {
            DataSetXML dataSetXML = CreateBaseXML(EventGameText);
            dataSetXML.TemplateFileNameRelativ = @"XML\Text\CIV4GameText_Colonization_Events_utf8_Template.xml";
            dataSetXML.TemplateFileNameAbsolute = PathHelper.GetFullAssetFileName(dataSetXML.TemplateFileNameRelativ);
            dataSetXML.TemplateFileNameConcrete = PathHelper.GetFullAssetFileName(@"XML\Text\CIV4GameText_Colonization_Events_utf8_Template_");
            dataSetXML.XmlRootNode = RootNodeEventTriggerInfo;
            dataSetXML.XmlDocumentTemplate = LoadFileXML(dataSetXML);
            return dataSetXML;
        }

        public DataSetPython CreateRandomEventStart()
        {
            DataSetPython dataSetPython = CreateBasePython(RandomEvent_Start);
            dataSetPython.TemplateFileNameRelativ = @"Python\EntryPoints\CvRandomEventInterface_Start_Template.py";
            dataSetPython.TemplateFileNameAbsolute = PathHelper.GetFullAssetFileName(dataSetPython.TemplateFileNameRelativ);
            dataSetPython.TemplateFileNameConcrete = PathHelper.GetFullAssetFileName(@"Python\EntryPoints\CvRandomEventInterface_Start_");
            dataSetPython.PythonContentTemplate = LoadFileText(dataSetPython);
            SetReplaceItems(dataSetPython);
            return dataSetPython;
        }
        public DataSetPython CreateRandomEventDone()
        {
            DataSetPython dataSetPython = CreateBasePython(RandomEvent_Done);
            dataSetPython.TemplateFileNameRelativ = @"Python\EntryPoints\CvRandomEventInterface_Done_Template.py";
            dataSetPython.TemplateFileNameAbsolute = PathHelper.GetFullAssetFileName(dataSetPython.TemplateFileNameRelativ);
            dataSetPython.TemplateFileNameConcrete = PathHelper.GetFullAssetFileName(@"Python\EntryPoints\CvRandomEventInterface_Done_");
            dataSetPython.PythonContentTemplate = LoadFileText(dataSetPython);
            SetReplaceItems(dataSetPython);
            return dataSetPython;
        }

        private DataSetXML CreateBaseXML( string templateName )
        {
            DataSetXML dataSetXML = new DataSetXML();
            dataSetXML.TemplateName = templateName;
            dataSetXML.TemplateFileExtension = FileExtensionXML;
            dataSetXML.TemplateFileNameConcrete = "";
            return dataSetXML;
        }

        private DataSetPython CreateBasePython(string templateName)
        {
            DataSetPython dataSetPython = new DataSetPython();
            dataSetPython.TemplateName = templateName;
            dataSetPython.TemplateFileExtension = FileExtensionPython;
            dataSetPython.TemplateFileNameConcrete = "";
            return dataSetPython;
        }

        private XmlDocument LoadFileXML( DataSetBase dataSetBase )
        {
            XMLFileUtility fileUtility = new XMLFileUtility();
            return fileUtility.Load(dataSetBase.TemplateFileNameAbsolute);
        }

        private String LoadFileText( DataSetBase dataSetBase )
        {
            TextFileUtility textFileLoader = new TextFileUtility();
            return textFileLoader.Load( dataSetBase.TemplateFileNameAbsolute );
        }

        private void SetReplaceItems( DataSetPython dataSetPython )
        {
            dataSetPython.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_NORMAL, "");
            dataSetPython.TemplateReplaceItems.Add(ReplaceItems.HARBOUR_UPPERCASE, "");
            dataSetPython.TemplateReplaceItems.Add(ReplaceItems.YIELD, "");
        }

    }
}
