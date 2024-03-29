﻿using System.Collections.Generic;

namespace WeThePeople_ModdingTool.DataSets
{
    public class DataSetBase
    {
        public string TemplateName;
        public string TemplateFileNameRelativ;
        public string TemplateFileNameAndPathAbsolute;
        public string TemplateFileExtension;
        public string OriginalFileName;
        public IDictionary<string, string> TemplateReplaceItems = new Dictionary<string, string>();
    }
}
