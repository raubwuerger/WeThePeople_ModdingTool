using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.FileUtilities;

namespace WeThePeople_ModdingTool.ContentInserter
{
    public class ContentInserterXML : ContentInserterBase
    {
        string uniqueNodeName;
        public string UniqueNodeName
        {
            set { uniqueNodeName = value; }
        }
        string nodeNameToInsert;
        public string NodeNameToInsert
        {
            set { nodeNameToInsert = value; }
        }

        string parentNodeToAppend;
        public string ParentNodeToAppend
        {
            set { parentNodeToAppend = value; }
        }
        public override bool Insert(XmlDocument content)
        {
            XmlDocument destination = XMLFileUtility.Load(FileName);
            XmlNodeList destinationNodes = destination.DocumentElement.GetElementsByTagName(uniqueNodeName);

            XmlNodeList nodesToInsert = content.DocumentElement.GetElementsByTagName(uniqueNodeName);

            if( true == Contains(nodesToInsert, destinationNodes) )
            {
                return false;
            }

            XmlNode xmlNodeDestination;
            XmlNode xmlNodeToInsert;
            if( ((XmlNode)content.DocumentElement).Name.Equals(parentNodeToAppend) )
            {
                xmlNodeToInsert = (XmlNode)content.DocumentElement;
                xmlNodeDestination = (XmlNode)destination.DocumentElement;
            }
            else
            {
                XmlNodeList xmlNodesToInsert = content.DocumentElement.GetElementsByTagName(parentNodeToAppend);
                if (xmlNodesToInsert.Count != 1)
                {
                    return false;
                }
                xmlNodeToInsert = xmlNodesToInsert[0];

                XmlNodeList xmlNodesDestination = destination.DocumentElement.GetElementsByTagName(parentNodeToAppend);
                if (xmlNodesDestination.Count != 1)
                {
                    return false;
                }
                xmlNodeDestination = xmlNodesDestination[0];
            }

            if( false == Insert(destination, xmlNodeDestination, xmlNodeToInsert) )
            {
                return false;
            }

            return XMLFileUtility.SaveOverwrite(FileName, destination);
        }

        private bool Contains(XmlNode nodeToInsert, XmlNodeList destinationNodes)
        {
            foreach (XmlNode node in destinationNodes)
            {
                if (node.Equals(nodeToInsert))
                {
                    return true;
                }
            }
            return false;
        }

        private bool Contains(XmlNodeList nodesToInsert, XmlNodeList destinationNodes)
        {
            foreach (XmlNode node in nodesToInsert)
            {
                if (Contains(node, destinationNodes))
                {
                    return true;
                }
            }
            return false;
        }

        private bool Insert(XmlDocument xmlDocumentDestination, XmlNode xmlNodeDestination, XmlNodeList xmlNodesToInsert)
        {
            foreach( XmlNode xmlNodeToInsert in xmlNodesToInsert )
            {
                XmlNode importedNode = xmlDocumentDestination.ImportNode(xmlNodeToInsert, true);
                xmlNodeDestination.AppendChild(importedNode);
            }
            return true;
        }

        private bool Insert(XmlDocument xmlDocumentDestination, XmlNode xmlNodeDestination, XmlNode xmlNodeToInsert )
        {
            XmlNode importedNode = xmlDocumentDestination.ImportNode(xmlNodeToInsert, true);
            xmlNodeDestination.AppendChild(importedNode);
            return true;
        }

        public override bool Insert(string content)
        {
            throw new NotImplementedException();
        }

    }
}
