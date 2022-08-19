using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Validators;

namespace WeThePeople_ModdingTool
{
    public class EventCreator
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
            if( false == StringValidator.IsNullOrWhiteSpace(harbour) )
            {
                return false;
            }

            if (false == StringValidator.IsNullOrWhiteSpace(yieldType))
            {
                return false;
            }

            return true;
        }
        private string CreateConcreteFileName(DataSetBase dataSetBase)
        {
            string concreteFileName = yieldType;
            concreteFileName += "_";
            concreteFileName += harbour.ToUpper();
            concreteFileName += dataSetBase.TemplateFileExtension;
            return dataSetBase.TemplateFileNameConcrete + concreteFileName;
        }
        private bool SaveFile(string fileName, string pythonFile)
        {
            TextFileUtility textFileUtility = new TextFileUtility();
            return textFileUtility.Save(fileName, pythonFile);
        }
        private bool SaveFile(string fileName, XmlDocument xmlDocument)
        {
            XMLFileUtility xmlFileUtility = new XMLFileUtility();
            return xmlFileUtility.Save(fileName, xmlDocument);
        }

    }
}
