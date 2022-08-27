using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.Validators;
using Serilog;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;
using System.Xml;

namespace WeThePeople_ModdingTool.Creators
{
    public class EventCreatorFilesPutTogether : EventCreatorBase
    {
        public static string RootNodeBase_Civ4GameText = "Civ4GameText";

        public static string RootNode_CIV4EVENTTRIGGERINFOS = "Civ4EventTriggerInfos";
        public static string Subnode_EventTriggerInfos = "EventTriggerInfos";

        public static string RootNode_CIV4EVENTINFOS = "Civ4EventInfos";
        public static string Subnode_EventInfos = "EventInfos";

        public override bool Create()
        {
            if (false == EventCreatorHelper.IsValid(this))
            {
                return false;
            }

            if ( false == ConcatenatePythonFiles() )
            {
                return false;
            }

            if( false == ConcatenateXMLFiles() )
            {
                return false;
            }

            return true;
        }

        private bool ConcatenatePythonFiles()
        {
            DataSetPython dataSetPythonStart = TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Start);
            DataSetPython dataSetPythonDone = TemplateRepository.Instance.FindByNamePython(DataSetFactory.RandomEvent_Done);

            if( false == DataSetValidator.ValidateFull(dataSetPythonStart) )
            {
                Log.Debug("DataSetPyton start is not valid!");
                return false;
            }
            if( false == DataSetValidator.ValidateFull(dataSetPythonDone) )
            {
                Log.Debug("DataSetPyton start is not valid!");
                return false;
            }

            string concatenatedFiles = dataSetPythonStart.PythonContentProcessed;
            concatenatedFiles += CommonVariables.CR;
            concatenatedFiles += dataSetPythonDone.PythonContentProcessed;

            string savePathExtended = PathHelper.CombinePaths(SavePath,dataSetPythonDone.BaseAssetPath);
            return TextFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(savePathExtended, EventCreatorHelper.CreateConcreteFileNamePutTogether( this, dataSetPythonStart)), concatenatedFiles );
        }

        private bool ConcatenateXMLFiles()
        {
            if( false == CreateCiv4GameText() )
            {
                return false;
            }

            if( false == CreateEventTriggerInfo() )
            {
                return false;
            }

            if( false == CreateEventInfo() )
            {
                return false;
            }

            return true;
        }

        private bool CreateCiv4GameText()
        {
            DataSetXML eventGameText = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventGameText);
            return XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(PathHelper.CombinePaths(SavePath,eventGameText.BaseAssetPath), EventCreatorHelper.CreateConcreteFileNamePutTogether(this, eventGameText)), eventGameText.XmlDocumentProcessed);
        }

        private bool CreateEventTriggerInfo()
        {
            List<DataSetXML> dataSetXMLs = new List<DataSetXML>();
            dataSetXMLs.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Start));
            dataSetXMLs.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done));

            List<XmlDocument> xmlDocuments = new List<XmlDocument>();
            xmlDocuments.AddRange(DataSetConverter.CreateList(dataSetXMLs));
            XmlDocument concatenated = Concatenate(xmlDocuments);
            if (null == concatenated)
            {
                return false;
            }

            return XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(PathHelper.CombinePaths(SavePath, dataSetXMLs[0].BaseAssetPath), EventCreatorHelper.CreateConcreteFileNamePutTogether(this, dataSetXMLs[0])), concatenated);
        }

        private bool CreateEventInfo()
        {
            List<DataSetXML> eventEventInfos = new List<DataSetXML>();
            eventEventInfos.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Start));
            foreach (KeyValuePair<string, DataSetXML> entry in TemplateRepository.Instance.XmlDocumentEventDone)
            {
                eventEventInfos.Add(entry.Value);
            }

            List<XmlDocument> xmlDocuments = new List<XmlDocument>();
            xmlDocuments.AddRange(DataSetConverter.CreateList(eventEventInfos));
            XmlDocument concatenated = Concatenate(xmlDocuments);
            if (null == concatenated)
            {
                return false;
            }

            return XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(PathHelper.CombinePaths(SavePath, eventEventInfos[0].BaseAssetPath), EventCreatorHelper.CreateConcreteFileNamePutTogether(this, eventEventInfos[0])), concatenated);
        }

        private XmlDocument Concatenate( List<XmlDocument> listToConcatenate )
        {
            if( listToConcatenate.Count < 0 )
            {
                return null;
            }

            if (listToConcatenate.Count == 1)
            {
                return listToConcatenate[0];
            }

            XmlDocument concatenated = listToConcatenate[0];
            for ( int i=0;i<listToConcatenate.Count - 1;i++ )
            {
                //TODO: Die Ausgangslage ist schon nicht sauber! Warum ist in der Liste ein null-Object?
                int newIndex = i + 1;
                if(listToConcatenate[newIndex] == null || listToConcatenate[newIndex].DocumentElement == null || newIndex >= listToConcatenate.Count )
                {
                    break;
                }
                concatenated = Concatenate(concatenated, listToConcatenate[newIndex]);
            }

            return concatenated;
        }

        private XmlDocument Concatenate( XmlDocument baseXML, XmlDocument addXML )
        {
            XmlNode importedDocument = baseXML.ImportNode(addXML.DocumentElement, true);
            baseXML.DocumentElement.AppendChild(importedDocument);
            return baseXML;
        }

    }
}
