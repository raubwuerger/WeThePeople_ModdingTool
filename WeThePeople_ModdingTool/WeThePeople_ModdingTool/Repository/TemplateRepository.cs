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
        private IDictionary<string, string> pythonFiles = new Dictionary< string, string>();

        public bool RegisterTemplate( DataSetXML dataSetXML )
        {
            try
            {
                if (true == xmlDocuments.ContainsKey(dataSetXML.TemplatName))
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
                return xmlDocuments.TryAdd(dataSetXML.TemplatName, dataSetXML);
            }
            catch( ArgumentNullException)
            {
                CommonMessageBox.Show_OK_Error("Wrong parameter!", "Key must not be null!");
                return false;
            }
        }

        public bool RegisterTemplate(string name, string pythonContent)
        {
            try
            {
                if (true == pythonFiles.ContainsKey(name))
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
                return pythonFiles.TryAdd(name, pythonContent);
            }
            catch (ArgumentNullException)
            {
                CommonMessageBox.Show_OK_Error("Wrong parameter!", "Key must not be null!");
                return false;
            }
        }
    }
}
