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
        public bool Create()
        {
            if( false == IsValid() )
            {
                return false;
            }

            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if( System.Windows.Forms.DialogResult.OK != dialog.ShowDialog() )
            {
                return false;
            }

            string baseBath = dialog.SelectedPath;

            foreach(KeyValuePair<string,DataSetXML> entry in TemplateRepository.Instance.XmlDocuments )
            {
                if( entry.Value.XmlDocumentProcessed == null )
                {
                    continue;
                }
                SaveFile( PathHelper.CombinePathAndFileName(baseBath, CreateConcreteFileName(entry.Value)), entry.Value.XmlDocumentProcessed );
            }

            foreach (KeyValuePair<string, DataSetPython> entry in TemplateRepository.Instance.PythonFiles)
            {
                SaveFile(PathHelper.CombinePathAndFileName(baseBath, CreateConcreteFileName(entry.Value)), entry.Value.PythonContentProcessed);
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
        private bool SaveFile(string fileName, string pythonFile)
        {
            TextFileUtility textFileUtility = new TextFileUtility();
            return textFileUtility.SaveCreatePath(fileName, pythonFile);
        }
        private bool SaveFile(string fileName, XmlDocument xmlDocument)
        {
            XMLFileUtility xmlFileUtility = new XMLFileUtility();
            return xmlFileUtility.SaveCreatePath(fileName, xmlDocument);
        }

    }
}
