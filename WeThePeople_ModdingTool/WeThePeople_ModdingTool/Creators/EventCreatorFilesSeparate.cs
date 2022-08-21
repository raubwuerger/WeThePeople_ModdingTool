using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.Creators;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool
{
    public class EventCreatorFilesSeparate : IEventCreator
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
            if( false == EventCreatorHelper.IsValid(this) )
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
                XMLFileUtility.SaveCreatePath( PathHelper.CombinePathAndFileName(savePathExtended, EventCreatorHelper.CreateConcreteFileName(this, entry.Value)), entry.Value.XmlDocumentProcessed);
            }

            foreach (KeyValuePair<string, DataSetXML> entry in TemplateRepository.Instance.XmlDocumentEventDone)
            {
                if (entry.Value.XmlDocumentProcessed == null)
                {
                    continue;
                }
                XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(savePathExtended, EventCreatorHelper.CreateConcreteFileName(this, entry.Value)), entry.Value.XmlDocumentProcessed);
            }

            foreach (KeyValuePair<string, DataSetPython> entry in TemplateRepository.Instance.PythonFiles)
            {
                TextFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(savePathExtended, EventCreatorHelper.CreateConcreteFileName(this, entry.Value)), entry.Value.PythonContentProcessed);
            }

            return true;
        }

    }
}
