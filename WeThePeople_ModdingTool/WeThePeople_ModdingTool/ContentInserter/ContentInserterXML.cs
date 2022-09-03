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

        private ListHelper listComparator = new ListHelper();
        
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

                ListHelper listHelper = new ListHelper();
                xmlNodesToInsert = listHelper.GetNotIncluded(originalNodes, xmlNodesToInsert);

                if (false == InsertNodes(originalDoc, xmlNodeDestination, xmlNodesToInsert))
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

        private bool Contains(XmlNodeList nodesToInsert, XmlNodeList destinationNodes)
        {
            ListComparatorFactory listComparatorFactory = new ListComparatorFactory();
            ListHelper listComparator = listComparatorFactory.CreateListComparatorGameEventText();
            return listComparator.Contains(nodesToInsert, destinationNodes);
        }

        private bool InsertNodes(XmlDocument xmlDocumentDestination, XmlNode xmlNodeDestination, XmlNodeList xmlNodesToInsert)
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
            xmlNodeDestination.AppendChild(importedNode);
            return true;
        }

        public override bool Insert(string content)
        {
            throw new NotImplementedException();
        }

    }
}
