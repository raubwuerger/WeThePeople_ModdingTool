using System;
using System.Collections.Generic;
using System.Xml;
using Serilog;
using WeThePeople_ModdingTool.DataSets;

namespace WeThePeople_ModdingTool
{
    public class XMLItemReplacer
    {
        private XmlDocument replacedXmlDocument;
        public XmlDocument ReplacedContent { get => replacedXmlDocument; }

        private DataSetXML dataSetXML;
        public XMLItemReplacer( DataSetXML dataSetXMLObject )
        {
            dataSetXML = dataSetXMLObject;
        }

        public bool Replace()
        {
            if( null == dataSetXML.XmlDocumentTemplate )
            {
                Log.Debug("XmlDocument is null!");
                return false;
            }

            if( true == dataSetXML.TemplateReplaceItems.Count <= 0 )
            {
                Log.Debug("ReplaceItems are empty in dataSet! " +dataSetXML.TemplateName);
                return false;
            }

            if(dataSetXML.XmlRootNode.Equals(String.Empty) )
            {
                Log.Debug("RootNode not set! --> RootNode");
                return false;
            }

            replacedXmlDocument = dataSetXML.XmlDocumentTemplate;
            Replace(replacedXmlDocument.DocumentElement.SelectNodes(dataSetXML.XmlRootNode));
            return true;
        }

        private void Replace( XmlNodeList nodes )
        {
            foreach (XmlNode node in nodes)
            {
                if( true == node.HasChildNodes )
                {
                    Replace(node.ChildNodes);
                }
                else 
                {
                    node.InnerText = replaceText(node.InnerText);
                }
            }
        }

        private string replaceText( string content )
        {
            string replacedText = content;
            foreach (KeyValuePair<string, string> entry in dataSetXML.TemplateReplaceItems)
            {
                replacedText = TextReplacer.Replace(replacedText, entry);
            }

            return replacedText;
        }
    }
}
