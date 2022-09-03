using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.FileUtilities;
using WeThePeople_ModdingTool.Helper;

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

        private ListHelperXML listComparator = new ListHelperXML();
        
        public override bool Insert(XmlDocument content)
        {
            XmlDocument originalDoc = XMLFileUtility.Load(FileName);
            XmlNodeList originalNodes = originalDoc.DocumentElement.GetElementsByTagName(uniqueNodeName);

            XmlNodeList nodesToInsert = content.DocumentElement.GetElementsByTagName(uniqueNodeName);

            if( true == Contains(nodesToInsert, originalNodes) )
            {
                return false;
            }

            XmlNode xmlNodeDestination;
            if( ((XmlNode)content.DocumentElement).Name.Equals(parentNodeToAppend) )
            {
                XmlNodeList xmlNodesToInsert = content.DocumentElement.GetElementsByTagName(uniqueNodeName);
                xmlNodeDestination = (XmlNode)originalDoc.DocumentElement;

                ListHelperXML listHelper = new ListHelperXML();
                xmlNodesToInsert = listHelper.GetNotIncluded(originalNodes, xmlNodesToInsert);

                if (false == InsertNodes(originalDoc, xmlNodeDestination, CreateParentList(xmlNodesToInsert)))
                {
                    return false;
                }
            }
            else
            {
                XmlNodeList xmlNodesToInsert = content.DocumentElement.GetElementsByTagName(parentNodeToAppend);
                if (xmlNodesToInsert.Count != 1)
                {
                    return false;
                }
                XmlNode xmlNodeToInsert = xmlNodesToInsert[0];

                XmlNodeList xmlNodesDestination = originalDoc.DocumentElement.GetElementsByTagName(parentNodeToAppend);
                if (xmlNodesDestination.Count != 1)
                {
                    return false;
                }
                xmlNodeDestination = xmlNodesDestination[0];
                if (false == InsertNodes(originalDoc, xmlNodeDestination, xmlNodeToInsert))
                {
                    return false;
                }
            }

            return XMLFileUtility.SaveOverwrite(FileName, originalDoc);
        }

        private List<XmlNode> CreateParentList( XmlNodeList xmlNodeList )
        {
            List<XmlNode> xmlNodes = new List<XmlNode>();
            foreach( XmlNode xmlNode in xmlNodeList )
            {
                xmlNodes.Add(xmlNode.ParentNode);
            }
            return xmlNodes;
        }

        private bool Contains(XmlNodeList nodesToInsert, XmlNodeList destinationNodes)
        {
            ListComparatorFactory listComparatorFactory = new ListComparatorFactory();
            ListHelperXML listComparator = listComparatorFactory.CreateListComparatorGameEventText();
            return listComparator.Contains(nodesToInsert, destinationNodes);
        }

        private bool InsertNodes(XmlDocument xmlDocumentDestination, XmlNode xmlNodeDestination, List<XmlNode> xmlNodesToInsert)
        {
            foreach( XmlNode xmlNodeToInsert in xmlNodesToInsert )
            {
                XmlNode importedNode = xmlDocumentDestination.ImportNode(xmlNodeToInsert, true);
                xmlNodeDestination.AppendChild(importedNode);
            }
            return true;
        }

        private bool InsertNodes(XmlDocument xmlDocumentDestination, XmlNode xmlNodeDestination, XmlNode xmlNodeToInsert )
        {
            XmlNode importedNode = xmlDocumentDestination.ImportNode(xmlNodeToInsert, true);
            while( importedNode.ChildNodes.Count > 0 )
            {
                xmlNodeDestination.AppendChild(importedNode.ChildNodes[0]);
            }
            return true;
        }

        public override bool Insert(string content)
        {
            throw new NotImplementedException();
        }

    }
}
