using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.Creators;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool
{
    public class EventCreatorFilesSeparate : IEventCreator
    {
        private string harbour;
        public string Harbour
        {
            set { harbour = value; }
        }
        private string yieldType;
        public string YieldType
        {
            set { yieldType = value; }
        }

        private string savePath;
        public string SavePath
        {
            set { savePath = value; }
        }

        public bool Create()
        {
            if( false == IsValid() )
            {
                return false;
            }

            string savePathExtended = PathHelper.CombineAssetPathShortAndFileName(savePath);

            foreach (KeyValuePair<string,DataSetXML> entry in TemplateRepository.Instance.XmlDocuments )
            {
                if( entry.Value.XmlDocumentProcessed == null )
                {
                    continue;
                }
                XMLFileUtility.SaveCreatePath( PathHelper.CombinePathAndFileName(savePathExtended, CreateConcreteFileName(entry.Value)), entry.Value.XmlDocumentProcessed);
            }

            foreach (KeyValuePair<string, DataSetXML> entry in TemplateRepository.Instance.XmlDocumentEventDone)
            {
                if (entry.Value.XmlDocumentProcessed == null)
                {
                    continue;
                }
                XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(savePathExtended, CreateConcreteFileName(entry.Value)), entry.Value.XmlDocumentProcessed);
            }

            foreach (KeyValuePair<string, DataSetPython> entry in TemplateRepository.Instance.PythonFiles)
            {
                TextFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(savePathExtended, CreateConcreteFileName(entry.Value)), entry.Value.PythonContentProcessed);
            }

            return true;
        }

        private bool IsValid()
        {
            if( true == StringValidator.IsNullOrWhiteSpace(harbour) )
            {
                return false;
            }

            if( true == StringValidator.IsNullOrWhiteSpace(yieldType) )
            {
                return false;
            }

            if( true == StringValidator.IsNullOrWhiteSpace(savePath) )
            {
                return false;
            }

            return true;
        }
        private string CreateConcreteFileName(DataSetBase dataSetBase)
        {
            string processedAppendix = yieldType;
            processedAppendix += "_";
            processedAppendix += harbour.ToUpper();
            processedAppendix += dataSetBase.TemplateFileExtension;
            return dataSetBase.TemplateFileNameProcessed + processedAppendix;
        }
    }
}
