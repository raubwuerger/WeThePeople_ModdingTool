using System.Xml;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Validators;
using Serilog;
using System;
using System.Diagnostics;

namespace WeThePeople_ModdingTool.Processors
{
    public class EventProcessor
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
        public string Process(DataSetPython dataSetPython)
        {
            if( false == IsInitialized() )
            {
                return String.Empty;
            }
            dataSetPython.TemplateReplaceItems[ReplaceItems.HARBOUR_NORMAL] = harbour;
            dataSetPython.TemplateReplaceItems[ReplaceItems.HARBOUR_UPPERCASE] = harbour.ToUpper();
            dataSetPython.TemplateReplaceItems[ReplaceItems.YIELD] = yieldType;

            PythonItemReplacer replacer = new PythonItemReplacer(dataSetPython);

            if (false == replacer.Replace())
            {
                return String.Empty;
            }

            return replacer.ReplacedString;
        }

        public XmlDocument Process(DataSetXML dataSetXML)
        {
            if (false == IsInitialized())
            {
                return new XmlDocument();
            }

            dataSetXML.TemplateReplaceItems[ReplaceItems.HARBOUR_NORMAL] = harbour;
            dataSetXML.TemplateReplaceItems[ReplaceItems.HARBOUR_UPPERCASE] = harbour.ToUpper();
            dataSetXML.TemplateReplaceItems[ReplaceItems.YIELD] = yieldType;

            XMLItemReplacer replacer = new XMLItemReplacer(dataSetXML);

            if (false == replacer.Replace())
            {
                return new XmlDocument();
            }

            return replacer.ReplacedContent;
        }

        private bool IsInitialized()
        {
            if( true == StringValidator.IsNullOrWhiteSpace(harbour) )
            {
                Debug.Assert(false, "EventProcessor is not Initialized! (harbour)");
                return false;
            }

            if (true == StringValidator.IsNullOrWhiteSpace(yieldType))
            {
                Debug.Assert(false, "EventProcessor is not Initialized! (yieldType)");
                return false;
            }

            return true;
        }

    }
}
