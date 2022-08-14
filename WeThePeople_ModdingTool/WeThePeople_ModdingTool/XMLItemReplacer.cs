using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
        public bool Replace( XmlDocument xmlDocument )
        {
            if( null == xmlDocument )
            {
                return false;
            }

            if( true == ReplaceItems.Count <= 0 )
            {
                return false;
            }

            replacedXmlDocument = xmlDocument;
            XmlNodeList nodes = replacedXmlDocument.DocumentElement.SelectNodes("/EventInfo");

            foreach (XmlNode node in nodes)
            {
                XmlNodeList childs = node.ChildNodes;
                foreach (XmlNode childNode in childs)
                {
                    string replacedText = replaceText(childNode.InnerText);
                    childNode.InnerText = replacedText;
                }
            }

            return true;
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
