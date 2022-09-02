using System;
using System.Collections.Generic;
using System.Text;

namespace WeThePeople_ModdingTool.DataSets
{
    public class DataSetBase
    {
        public string TemplateName;
        public string TemplatePathRelative;
        public string TemplateFileNameRelativ;
        public string TemplateFileNameAndPathAbsolute;
        public string TemplateFileExtension;
        public string TemplateFileNameProcessed;
        public string TemplateFileNameAndPathProcessed;
        public string BaseAssetPath;
        public string OriginalFileName;
        public IDictionary<string, string> TemplateReplaceItems = new Dictionary<string, string>();
    }
}
