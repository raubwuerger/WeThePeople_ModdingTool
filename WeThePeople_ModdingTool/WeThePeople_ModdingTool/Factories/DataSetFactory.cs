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
        private static string FileExtension = ".xml";
        private static string RootNodeEventInfo = "/EventInfo";
        private static string RootNodeEventTriggerInfo = "/EventTriggerInfo";
        public DataSetXML CreateEventInfos_Start()
        {
            DataSetXML dataSetXML = CreateBase("EventInfos_Start");
            dataSetXML.TemplatePathRelativ = @"XML\Events\CIV4EventInfos_Start_Template.xml";
            dataSetXML.TemplatePathAbsolute = PathHelper.GetFullAssetPath(dataSetXML.TemplatePathRelativ);
            dataSetXML.RootNode = RootNodeEventInfo;
            dataSetXML.XmlDocumentObject = LoadFile(dataSetXML);
            return dataSetXML;
        }

        public DataSetXML CreateEventInfos_Done()
        {
            DataSetXML dataSetXML = CreateBase("EventInfos_Done");
            dataSetXML.TemplatePathRelativ = @"XML\Events\CIV4EventInfos_Done_Template.xml";
            dataSetXML.TemplatePathAbsolute = PathHelper.GetFullAssetPath(dataSetXML.TemplatePathRelativ);
            dataSetXML.RootNode = RootNodeEventInfo;
            dataSetXML.XmlDocumentObject = LoadFile(dataSetXML);
            return dataSetXML;
        }

        public DataSetXML CreateEventTriggerInfos_Start()
        {
            DataSetXML dataSetXML = CreateBase("EventTriggerInfos_Start");
            dataSetXML.TemplatePathRelativ = @"XML\Events\CIV4EventTriggerInfos_Start_Template.xml";
            dataSetXML.TemplatePathAbsolute = PathHelper.GetFullAssetPath(dataSetXML.TemplatePathRelativ);
            dataSetXML.RootNode = RootNodeEventTriggerInfo;
            dataSetXML.XmlDocumentObject = LoadFile(dataSetXML);
            return dataSetXML;
        }
        public DataSetXML CreateEventTriggerInfos_Done()
        {
            DataSetXML dataSetXML = CreateBase("EventTriggerInfos_Done");
            dataSetXML.TemplatePathRelativ = @"XML\Events\CIV4EventTriggerInfos_Done_Template.xml";
            dataSetXML.TemplatePathAbsolute = PathHelper.GetFullAssetPath(dataSetXML.TemplatePathRelativ);
            dataSetXML.RootNode = RootNodeEventTriggerInfo;
            dataSetXML.XmlDocumentObject = LoadFile(dataSetXML);
            return dataSetXML;
        }

        public DataSetXML CreateEventGameText()
        {
            DataSetXML dataSetXML = CreateBase("EventGameText");
            dataSetXML.TemplatePathRelativ = @"XML\Text\CIV4GameText_Colonization_Events_utf8_Template.xml";
            dataSetXML.TemplatePathAbsolute = PathHelper.GetFullAssetPath(dataSetXML.TemplatePathRelativ);
            dataSetXML.RootNode = RootNodeEventTriggerInfo;
            dataSetXML.XmlDocumentObject = LoadFile(dataSetXML);
            return dataSetXML;
        }

        private DataSetXML CreateBase( string templateName )
        {
            DataSetXML dataSetXML = new DataSetXML();
            dataSetXML.TemplatName = templateName;
            dataSetXML.TemplatFileExtension = FileExtension;
            dataSetXML.ConcreteFileName = "";
            return dataSetXML;
        }

        private XmlDocument LoadFile( DataSetXML dataSetXML )
        {
            XMLFileUtility fileUtility = new XMLFileUtility();
            return fileUtility.Load(dataSetXML.TemplatePathAbsolute);

        }
    }
}
