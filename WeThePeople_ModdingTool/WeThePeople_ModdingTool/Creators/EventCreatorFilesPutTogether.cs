using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.Validators;
using Serilog;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;

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

            return TextFileUtility.SaveCreatePath(PathHelper.CombineAssetPathShortAndFileName(savePath), concatenatedFiles);

        }

        private bool ConcatenateXMLFiles()
        {
            DataSetXML eventGameText = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventGameText);
            XMLFileUtility.SaveCreatePath(PathHelper.CombinePathAndFileName(PathHelper.CombineAssetPathShortAndFileName(savePath), EventCreatorHelper.CreateConcreteFileName(this, eventGameText)), eventGameText.XmlDocumentProcessed);

            return false;
        }
    }
}
