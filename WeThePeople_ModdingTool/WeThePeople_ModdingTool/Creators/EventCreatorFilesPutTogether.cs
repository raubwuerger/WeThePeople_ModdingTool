using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.Validators;
using Serilog;
using WeThePeople_ModdingTool.FileUtilities;

namespace WeThePeople_ModdingTool.Creators
{
    public class EventCreatorFilesPutTogether : IEventCreator
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
            return ConcatenatePythonFiles();
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

            return TextFileUtility.SaveCreatePath(savePath, concatenatedFiles);

        }

        private bool IsValid()
        {
            if (true == StringValidator.IsNullOrWhiteSpace(harbour))
            {
                return false;
            }

            if (true == StringValidator.IsNullOrWhiteSpace(yieldType))
            {
                return false;
            }

            if (true == StringValidator.IsNullOrWhiteSpace(savePath))
            {
                return false;
            }

            return true;
        }

    }
}
