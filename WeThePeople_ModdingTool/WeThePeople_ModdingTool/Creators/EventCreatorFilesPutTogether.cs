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
    public class EventCreatorFilesPutTogether : IEventCreator
    {
        private string harbour;
        public string Harbour
        {
            get { return harbour; }
            set { harbour = value; }
        }
        private string yieldType;
        public string YieldType
        {
            get { return yieldType; }
            set { yieldType = value; }
        }

        private string savePath;
        public string SavePath
        {
            get { return savePath; }
            set { savePath = value; }
        }

        public bool Create()
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

            string savePathExtended = PathHelper.CombineAssetPathShortAndFileName(savePath);
            return TextFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(savePathExtended, EventCreatorHelper.CreateConcreteFileNamePutTogether(dataSetPythonStart)), concatenatedFiles );
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

            return true;
        }

        private bool CreateCiv4GameText()
        {
            DataSetXML eventGameText = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventGameText);
            return XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(PathHelper.CombineAssetPathShortAndFileName(savePath), EventCreatorHelper.CreateConcreteFileNamePutTogether(eventGameText)), eventGameText.XmlDocumentProcessed);
        }

        private bool CreateEventTriggerInfo()
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement root = xmlDocument.CreateElement(DataSetFactory.RootNode_EventTriggerInfos);
            xmlDocument.AppendChild(root);

            List<DataSetXML> dataSetXMLs = new List<DataSetXML>();
            dataSetXMLs.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Start));
            dataSetXMLs.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done));

            //Todo Insert xmlDocument at first
            XmlDocument concatenated = Concatenate(DataSetConverter.CreateList(dataSetXMLs));
            if (null == concatenated)
            {
                return false;
            }

            return XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(PathHelper.CombineAssetPathShortAndFileName(savePath), EventCreatorHelper.CreateConcreteFileNamePutTogether(dataSetXMLs[0])), concatenated);
        }

        private bool CreateEventInfo()
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement root = xmlDocument.CreateElement(DataSetFactory.RootNode_EventTriggerInfos);
            xmlDocument.AppendChild(root);

            List<DataSetXML> eventEventInfos = new List<DataSetXML>();
            eventEventInfos.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Start));
            foreach (KeyValuePair<string, DataSetXML> entry in TemplateRepository.Instance.XmlDocumentEventDone)
            {
                eventEventInfos.Add(entry.Value);
            }

            //Todo Insert xmlDocument at first
            XmlDocument concatenated = Concatenate( DataSetConverter.CreateList(eventEventInfos) );
            if (null == concatenated)
            {
                return false;
            }

            return XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(PathHelper.CombineAssetPathShortAndFileName(savePath), EventCreatorHelper.CreateConcreteFileNamePutTogether(eventEventInfos[0])), concatenated);
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
                concatenated = Concatenate(concatenated, listToConcatenate[i+1]);
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
