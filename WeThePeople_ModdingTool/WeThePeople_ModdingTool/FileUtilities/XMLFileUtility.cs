using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class XMLFileUtility
    {
        string FileName;

        public XmlDocument Load( string fileName )
        {
            if (false == File.Exists(fileName))
            {
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_OPEN_CAPTION, CommonVariables.MESSAGE_BOX_FILE_DOESNT_EXIST_CR + fileName);
                return null;
            }
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
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_OPEN_CAPTION, CommonVariables.MESSAGE_BOX_EXCEPTION_CR + fileName + CommonVariables.CR + ex.Message);
                return null;
            }
        }

        public bool Save( string fileName, XmlDocument xmlDocument )
        {
            if (true == File.Exists(fileName))
            {
                if (MessageBoxResult.No == CommonMessageBox.Show_YesNo(CommonVariables.MESSAGE_BOX_UNABLE_SAVE_CAPTION, CommonVariables.MESSAGE_BOX_OVERWRITE_CR + fileName))
                {
                    return false;
                }
            }
            try
            {
                xmlDocument.Save(fileName);
                return true;
            }
            catch (Exception ex)
            {
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_SAVE_CAPTION, CommonVariables.MESSAGE_BOX_EXCEPTION_CR + fileName + CommonVariables.CR + ex.Message);
                return false;
            }

        }
    }
}
