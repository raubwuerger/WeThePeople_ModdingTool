﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool.DataSets
{
    public class DataSetXML : DataSetBase
    {
        public XmlDocument XmlDocumentTemplate;
        public XmlDocument XmlDocumentProcessed;
        public string XmlSelectNode;
        public string XmlRootNode;
        public string XmlParentNode;
        public string XmlInsertNode;
        public string XmlUniqueNode;
    }
}
