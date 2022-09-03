using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool.Helper
{
    public class ListHelperXML
    {
        private List<string> ignoreList = new List<string>();
        public List<string> IgnoreList
        {
            get { return ignoreList; }
            set { ignoreList = value; }
        }

        public bool Contains(XmlNodeList nodesToInsert, XmlNodeList destinationNodes)
        {
            foreach (XmlNode node in nodesToInsert)
            {
                if (ignoreList.Contains(node.InnerText))
                {
                    continue;
                }

                if (Contains(node, destinationNodes))
                {
                    return true;
                }
            }
            return false;
        }
        public bool Contains(XmlNode nodeToInsert, XmlNodeList destinationNodes)
        {
            foreach (XmlNode node in destinationNodes)
            {
                if (ignoreList.Contains(node.InnerText))
                {
                    continue;
                }
                if (node.InnerText.Equals(nodeToInsert.InnerText))
                {
                    return true;
                }
            }
            return false;
        }

        public XmlNodeList GetNotIncluded(XmlNodeList originalList, XmlNodeList listToInsert)
        {
            List<XmlNode> listToRemove = new List<XmlNode>();
            for (int i = listToInsert.Count - 1; i >= 0; i--)
            {
                if (true == Containes(originalList, listToInsert[i]))
                {
                    listToRemove.Add(listToInsert[i]);
                }
            }
            RemoveXmlNodes(listToRemove);
            return listToInsert;
        }

        private void RemoveXmlNodes( List<XmlNode> xmlNodes )
        {
            foreach( XmlNode xmlNode in xmlNodes )
            {
                XmlNode parentNode = xmlNode.ParentNode;
                parentNode.ParentNode.RemoveChild(parentNode);
            }
        }

        private bool Containes(XmlNodeList originalList, XmlNode nodeToInsert)
        {
            foreach (XmlNode node in originalList)
            {
                if (node.InnerText.Equals(nodeToInsert.InnerText))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
