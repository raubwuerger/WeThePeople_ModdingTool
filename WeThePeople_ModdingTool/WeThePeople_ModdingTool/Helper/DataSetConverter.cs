using System.Collections.Generic;
using System.Xml;
using WeThePeople_ModdingTool.DataSets;

namespace WeThePeople_ModdingTool.Helper
{
    public class DataSetConverter
    {
        public static List<XmlDocument> CreateList(List<DataSetXML> dataSetXMLs)
        {
            List<XmlDocument> documents = new List<XmlDocument>();
            foreach (DataSetXML entry in dataSetXMLs)
            {
                documents.Add(entry.XmlDocumentProcessed);
            }
            return documents;
        }
    }
}
