using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;

namespace WeThePeople_ModdingTool.ContentInserter
{
    public class ContentInserterFactory
    {
        public static ContentInserterBase CreateContentInserterPython(string fileName)
        {
            ContentInserterBase contentInserterBase = new ContentInserterPython();
            contentInserterBase.FileName = fileName;
            return contentInserterBase;
        }

        public static ContentInserterBase CreateContentInserterByXmlDocument( string fileName, DataSetXML dataSetXML )
        {
            return Create(fileName, dataSetXML);
        }

        public static ContentInserterBase Create(string fileName, DataSetXML dataSetXML)
        {
            ContentInserterXML contentInserterBase = new ContentInserterXML();
            contentInserterBase.FileName = fileName;
            contentInserterBase.UniqueNodeName = dataSetXML.XmlUniqueNode;
            contentInserterBase.NodeNameToInsert = dataSetXML.XmlInsertNode;
            contentInserterBase.ParentNodeToAppend = dataSetXML.XmlParentNode;
            return contentInserterBase;
        }
    }
}
