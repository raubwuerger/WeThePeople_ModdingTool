using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool.IO
{
    class XMLFileParser
    {
        public XmlDocument LoadFile( string fileName )
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            String nodeName;
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                nodeName = node.InnerText; //or loop through its children as well
            }
            return doc;
        }
    }
}
