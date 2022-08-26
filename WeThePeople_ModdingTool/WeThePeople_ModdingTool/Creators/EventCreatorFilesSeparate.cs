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
        public override bool Create()
        {
            if( false == EventCreatorHelper.IsValid(this) )
            {
                return false;
            }

            string savePathExtended = PathHelper.CombineAssetPathShortAndFileName(SavePath);

            foreach (KeyValuePair<string,DataSetXML> entry in TemplateRepository.Instance.XmlDocuments )
            {
                if( entry.Value.XmlDocumentProcessed == null )
                {
                    continue;
                }
                XMLFileUtility.SaveCreatePath( PathHelper.CombinePathAndFileName(savePathExtended, EventCreatorHelper.CreateConcreteFileNameSeparate(this, entry.Value)), entry.Value.XmlDocumentProcessed);
            }

            foreach (KeyValuePair<string, DataSetXML> entry in TemplateRepository.Instance.XmlDocumentEventDone)
            {
                if (entry.Value.XmlDocumentProcessed == null)
                {
                    continue;
                }
                XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(savePathExtended, EventCreatorHelper.CreateConcreteFileNameSeparate(this, entry.Value)), entry.Value.XmlDocumentProcessed);
            }

            foreach (KeyValuePair<string, DataSetPython> entry in TemplateRepository.Instance.PythonFiles)
            {
                TextFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(savePathExtended, EventCreatorHelper.CreateConcreteFileNameSeparate(this, entry.Value)), entry.Value.PythonContentProcessed);
            }

            return true;
        }

    }
}
