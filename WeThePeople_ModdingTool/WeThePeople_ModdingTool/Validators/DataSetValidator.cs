using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.DataSets;
using Serilog;

namespace WeThePeople_ModdingTool.Validators
{
    public class DataSetValidator
    {
        public static bool Validate( DataSetBase dataSetBase )
        {
            if( null == dataSetBase )
            {
                Log.Debug("DataSetBase is null!");
                return false;
            }

            bool validationOK = true;
            if( StringValidator.IsNullOrWhiteSpace(dataSetBase.TemplateName) )
            {
                Log.Debug("DataSetBase.TemplateName is invalid!");
                validationOK = false;
            }

            if (StringValidator.IsNullOrWhiteSpace(dataSetBase.TemplateFileNameRelativ))
            {
                Log.Debug("DataSetBase.TemplateFileNameRelativ is invalid!");
                validationOK = false;
            }

            if (StringValidator.IsNullOrWhiteSpace(dataSetBase.TemplateFileNameAbsolute))
            {
                Log.Debug("DataSetBase.TemplateFileNameAbsolute is invalid!");
                validationOK = false;
            }

            if (StringValidator.IsNullOrWhiteSpace(dataSetBase.TemplateFileExtension))
            {
                Log.Debug("DataSetBase.TemplateFileExtension is invalid!");
                validationOK = false;
            }

            if (StringValidator.IsNullOrWhiteSpace(dataSetBase.TemplateFileNameConcrete))
            {
                Log.Debug("DataSetBase.TemplateFileNameConcrete is invalid!");
                validationOK = false;
            }

            return validationOK;
        }

        public static bool Validate( DataSetXML dataSetXML )
        {
            bool validationOK = true;
            if( false == Validate((DataSetBase)dataSetXML) )
            {
                validationOK = false;
            }

            if( null == dataSetXML.XmlDocumentTemplate )
            {
                Log.Debug("DataSetXML.XmlDocumentObject is null!");
                validationOK = false;
            }

            if( StringValidator.IsNullOrWhiteSpace(dataSetXML.XmlRootNode) )
            {
                Log.Debug("DataSetXML.XmlRootNode is invalid!");
                validationOK = false;
            }
            return validationOK;
        }

        public static bool Validate( DataSetPython dataSetPython )
        {
            bool validationOK = true;
            if (false == Validate((DataSetBase)dataSetPython))
            {
                validationOK = false;
            }

            if (StringValidator.IsNullOrWhiteSpace(dataSetPython.PythonContentTemplate))
            {
                Log.Debug("DataSetXML.PythonContentTemplate is invalid!");
                validationOK = false;
            }

            return validationOK;
        }
    }
}
