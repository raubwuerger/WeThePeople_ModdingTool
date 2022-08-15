using System;
using System.Collections.Generic;
using System.Xml;
using Serilog;

namespace WeThePeople_ModdingTool
{
    public class XMLItemReplacer
    {
        private IDictionary<string, string> replaceItems = new Dictionary<string, string>();
        public IDictionary<string, string> ReplaceItems
        {
            get { return replaceItems; }
            set { replaceItems = value; }
        }

        private XmlDocument replacedXmlDocument;

        public XmlDocument ReplacedContent { get => replacedXmlDocument; }

        private string rootNode = String.Empty;
        
        //"/EventInfo"
        public string RootNode { get => rootNode; set => rootNode = value; }

        public bool Replace( XmlDocument xmlDocument )
        {
            if( null == xmlDocument )
            {
                Log.Debug("XmlDocument is null!");
                return false;
            }

            if( true == ReplaceItems.Count <= 0 )
            {
                Log.Debug("ReplaceItems are not set! --> ReplaceItems");
                return false;
            }

            if( rootNode.Equals(String.Empty) )
            {
                Log.Debug("RootNode not set! --> RootNode");
                return false;
            }

            replacedXmlDocument = xmlDocument;
            Replace(replacedXmlDocument.DocumentElement.SelectNodes(rootNode));
            return true;
        }

        private void Replace( XmlNodeList nodes )
        {
            foreach (XmlNode node in nodes)
            {
                XmlNodeList childs = node.ChildNodes;
                foreach (XmlNode childNode in childs)
                {
                    if( true == childNode.HasChildNodes )
                    {
                        Replace(childNode.ChildNodes);
                    }
                    else 
                    {
                        childNode.InnerText = replaceText(childNode.InnerText);
                    }
                }
            }
        }

        private string replaceText( string content )
        {
            string replacedText = content;
            foreach (KeyValuePair<string, string> entry in ReplaceItems)
            {
                replacedText = TextReplacer.replace(replacedText, entry);
            }

            return replacedText;
        }
    }
}
