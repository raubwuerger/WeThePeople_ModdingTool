using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class XMLFileLoader
    {
        public XmlDocument LoadFile( string fileName )
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                String nodeName;
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    nodeName = node.InnerText;
                }
                return doc;
            }
            catch( Exception ex )
            {
                return null;
            }
        }
    }
}
