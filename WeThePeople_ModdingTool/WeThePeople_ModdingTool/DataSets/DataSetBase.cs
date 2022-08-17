using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool.DataSets
{
    public class DataSetBase
    {
        public string TemplatName;
        public string TemplateFileNameRelativ;
        public string TemplateFileNameAbsolute;
        public string TemplateFileExtension;
        public string TemplateFileNameConcrete;
        public IDictionary<string, string> TemplateReplaceItems = new Dictionary<string, string>();
    }
}
