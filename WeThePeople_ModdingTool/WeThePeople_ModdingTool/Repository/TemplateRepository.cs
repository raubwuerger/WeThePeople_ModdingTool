using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.FileUtilities;

namespace WeThePeople_ModdingTool
{
    public sealed class TemplateRepository
    {

        private static readonly TemplateRepository instance = new TemplateRepository();

        static TemplateRepository()
        {
        }

        public static TemplateRepository Instance
        {
            get
            {
                return instance;
            }
        }

        private IDictionary<string, DataSetXML> xmlDocuments = new Dictionary<string, DataSetXML>();
        public System.Collections.Generic.IDictionary<string, WeThePeople_ModdingTool.DataSets.DataSetXML> XmlDocuments
        {
            get { return xmlDocuments; }
        }
        private IDictionary<string, DataSetPython> pythonFiles = new Dictionary< string, DataSetPython>();
        public System.Collections.Generic.IDictionary<string, WeThePeople_ModdingTool.DataSets.DataSetPython> PythonFiles
        {
            get { return pythonFiles; }
        }

        public bool RegisterTemplate(List<DataSetXML> dataSetXMLs)
        {
            foreach( DataSetXML entry in dataSetXMLs )
            {
                if( false == RegisterTemplate(entry) )
                {
                    return false;
                }
            }

            return true;
        }

        public bool RegisterTemplate( DataSetXML dataSetXML )
        {
            try
            {
                if (true == xmlDocuments.ContainsKey(dataSetXML.TemplateName))
                {
                    return false;
                }
            }
            catch (ArgumentNullException)
            {
                CommonMessageBox.Show_OK_Error("Wrong parameter!","Key must not be null!");
                return false;
            }

            try
            {
                return xmlDocuments.TryAdd(dataSetXML.TemplateName, dataSetXML);
            }
            catch( ArgumentNullException)
            {
                CommonMessageBox.Show_OK_Error("Wrong parameter!", "Key must not be null!");
                return false;
            }
        }

        public bool RegisterTemplate( DataSetPython dataSetPython )
        {
            try
            {
                if (true == pythonFiles.ContainsKey(dataSetPython.TemplateName))
                {
                    return false;
                }
            }
            catch (ArgumentNullException)
            {
                CommonMessageBox.Show_OK_Error("Wrong parameter!", "Key must not be null!");
                return false;
            }

            try
            {
                return pythonFiles.TryAdd(dataSetPython.TemplateName, dataSetPython);
            }
            catch (ArgumentNullException)
            {
                CommonMessageBox.Show_OK_Error("Wrong parameter!", "Key must not be null!");
                return false;
            }
        }
        public DataSetXML FindByNameXML( string name )
        {
            DataSetXML dataSetXML;
            xmlDocuments.TryGetValue(name, out dataSetXML);
            return dataSetXML;
        }

        public DataSetPython FindByNamePython(string name)
        {
            DataSetPython dataSetPython;
            pythonFiles.TryGetValue(name, out dataSetPython);
            return dataSetPython;
        }

        public void Reset()
        {
            foreach(KeyValuePair<string, DataSetXML> entry in xmlDocuments)
            {
                entry.Value.XmlDocumentProcessed = new XmlDocument();
            }

            foreach(KeyValuePair<string, DataSetPython> entry in pythonFiles)
            {
                entry.Value.PythonContentProcessed = String.Empty;
            }
        }
    }
}
