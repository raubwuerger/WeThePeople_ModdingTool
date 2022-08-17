using System;
using System.Collections.Generic;
using System.Text;
using WeThePeople_ModdingTool.DataSets;

namespace WeThePeople_ModdingTool
{
    public class PythonItemReplacer
    {
        private DataSetPython dataSetPython;

        public PythonItemReplacer( DataSetPython dataSetPythonObject )
        {
            dataSetPython = dataSetPythonObject;
        }

        public string ReplacedString { get => replacedString; }

        string replacedString;

        public void Init()
        {
            replacedString = null;
        }

        public bool Replace()
        {
            if (null == dataSetPython)
            {
                return false;
            }

            if (true == dataSetPython.TemplateReplaceItems.Count <= 0)
            {
                return false;
            }

            replacedString = dataSetPython.PythonContent;
            foreach (KeyValuePair<string, string> entry in dataSetPython.TemplateReplaceItems)
            {
                replacedString = TextReplacer.replace(replacedString, entry);
            }

            return true;
        }
    }
}
