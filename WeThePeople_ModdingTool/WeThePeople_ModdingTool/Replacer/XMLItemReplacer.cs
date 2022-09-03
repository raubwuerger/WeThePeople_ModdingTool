using System;
using System.Collections.Generic;
using System.Xml;
using Serilog;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.FileUtilities;

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

            if(dataSetXML.XmlSelectNode.Equals(String.Empty) )
            {
                Log.Debug("RootNode not set! --> RootNode");
                return false;
            }

            replacedXmlDocument = (XmlDocument)dataSetXML.XmlDocumentTemplate.Clone();
            XmlNode concreteNode = GetConcreteNode(dataSetXML);
            if ( null == concreteNode )
            {
                Log.Debug("Concrete node not found! " + dataSetXML.XmlInsertNode);
                return false;
            }
            Replace(concreteNode.ChildNodes);
            return true;
        }

        //Hack for CIV4GameText_Colonization_Events_utf8.xml. Because root node and parent node are identical!
        private XmlNode GetConcreteNode( DataSetXML dataSetXML )
        {
            XmlNodeList rootNodes = replacedXmlDocument.DocumentElement.SelectNodes(dataSetXML.XmlSelectNode);
            XmlNode concreteNode = XMLHelper.FindNodeByName(rootNodes, dataSetXML.XmlInsertNode);
            if ( dataSetXML.XmlParentNode.Equals(dataSetXML.XmlRootNode) )
            {
                return concreteNode.ParentNode;
            }
            else 
            {
                return concreteNode;
            }
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
                    node.InnerText = ReplaceText(node.InnerText);
                }
            }
        }

        private string ReplaceText( string content )
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
