using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool.ContentInserter
{
    public abstract class ContentInserterBase
    {
        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public abstract bool Insert(string content);

        public abstract bool Insert(XmlNodeList content);

    }
}
