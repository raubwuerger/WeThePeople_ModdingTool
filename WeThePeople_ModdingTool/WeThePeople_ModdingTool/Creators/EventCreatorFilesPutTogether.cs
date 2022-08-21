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
            DataSetXML eventGameText = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventGameText);
            XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(PathHelper.CombineAssetPathShortAndFileName(savePath), EventCreatorHelper.CreateConcreteFileNamePutTogether(eventGameText)), eventGameText.XmlDocumentProcessed);

            {
                List<DataSetXML> eventTriggerInfos = new List<DataSetXML>();
                eventTriggerInfos.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Start));
                eventTriggerInfos.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done));

                XmlDocument concatenated = Concatenate(eventTriggerInfos);
                if ( null == concatenated )
                {
                    return false;
                }

                XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(PathHelper.CombineAssetPathShortAndFileName(savePath), EventCreatorHelper.CreateConcreteFileNamePutTogether(eventTriggerInfos[0])),concatenated);
            }

            {
                List<DataSetXML> eventEventInfos = new List<DataSetXML>();
                eventEventInfos.Add(TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventInfos_Start));
                foreach ( KeyValuePair<string, DataSetXML> entry in TemplateRepository.Instance.XmlDocumentEventDone )
                {
                    eventEventInfos.Add(entry.Value);
                }

                XmlDocument concatenated = Concatenate(eventEventInfos);
                if (null == concatenated)
                {
                    return false;
                }

                XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(PathHelper.CombineAssetPathShortAndFileName(savePath), EventCreatorHelper.CreateConcreteFileNamePutTogether(eventEventInfos[0])), concatenated);
            }

            return true;
        }

        private XmlDocument Concatenate( List<DataSetXML> listToConcatenate )
        {
            if( listToConcatenate.Count < 0 )
            {
                return null;
            }

            if (listToConcatenate.Count == 1)
            {
                return listToConcatenate[0].XmlDocumentProcessed;
            }

            XmlDocument concatenated = listToConcatenate[0].XmlDocumentProcessed;
            for ( int i=0;i<listToConcatenate.Count - 1;i++ )
            {
                concatenated = Concatenate(concatenated, listToConcatenate[i+1].XmlDocumentProcessed);
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
