using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class XMLFileUtility
    {
        string FileName;

        public XmlDocument Load( string fileName )
        {
            try
            {
                FileName = fileName;
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                String nodeName;
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    nodeName = node.InnerText;
                }
                return doc;
            }
            catch( Exception ex )
            {
                ShowMessageBox(ex.Message);
                return null;
            }
        }
        private void ShowMessageBox(string fileName)
        {
            string message = FileName + "\r\n" + fileName;
            CommonMessageBox.Show_OK_Error("Unable to open file!", message);

        }
    }
}
