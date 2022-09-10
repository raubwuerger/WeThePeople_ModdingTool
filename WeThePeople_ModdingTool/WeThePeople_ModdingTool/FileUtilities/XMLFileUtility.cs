using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;
using WeThePeople_ModdingTool.DataSets;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class XMLFileUtility
    {
        static string FileName;

        public static XmlDocument Load(string fileName)
        {
            if (false == File.Exists(fileName))
            {
                Log.Warning(CommonVariables.MESSAGE_BOX_FILE_DOESNT_EXIST + CommonVariables.COLON_BLANK + fileName);
                return null;
            }
            try
            {
                Log.Debug("Loading file: " + fileName);
                FileName = fileName;
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = false;
                doc.Load(fileName);
                return doc;
            }
            catch (Exception ex)
            {
                Log.Error(CommonVariables.MESSAGE_BOX_EXCEPTION + CommonVariables.BLANK_MINUS_BLANK + ex.Message);
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_OPEN_CAPTION, CommonVariables.MESSAGE_BOX_EXCEPTION_CR + ex.Message);
                return null;
            }
        }

        public static bool Save(string fileName, XmlDocument xmlDocument)
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
                SaveFormattedXml(xmlDocument,fileName, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(CommonVariables.MESSAGE_BOX_EXCEPTION + CommonVariables.BLANK_MINUS_BLANK + ex.Message);
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_SAVE_CAPTION, CommonVariables.MESSAGE_BOX_EXCEPTION_CR + ex.Message);
                return false;
            }

        }
        public static bool SaveOverwrite(string fileName, XmlDocument xmlDocument)
        {
            try
            {
                SaveFormattedXml(xmlDocument, fileName, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(CommonVariables.MESSAGE_BOX_EXCEPTION + CommonVariables.BLANK_MINUS_BLANK + ex.Message);
                CommonMessageBox.Show_OK_Error(CommonVariables.MESSAGE_BOX_UNABLE_SAVE_CAPTION, CommonVariables.MESSAGE_BOX_EXCEPTION_CR + ex.Message);
                return false;
            }

        }
        public static bool SaveCreatePath(string fileName, XmlDocument xmlDocument)
        {
            string path = Path.GetDirectoryName(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            return Save(fileName, xmlDocument);
        }

        public static bool SaveCreatePathOverwrite(string fileName, XmlDocument xmlDocument)
        {
            string path = Path.GetDirectoryName(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            return SaveOverwrite(fileName, xmlDocument);
        }

        public static void SaveFormattedXml(XmlDocument doc, String outputPath, Encoding encoding)
        {
            Log.Debug("Saving file: " + outputPath);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            settings.NewLineChars = "\r\n";
            settings.NewLineHandling = NewLineHandling.Replace;

            using (MemoryStream memstream = new MemoryStream())
            using (StreamWriter sr = new StreamWriter(memstream, encoding))
            using (XmlWriter writer = XmlWriter.Create(sr, settings))
            using (FileStream fileWriter = new FileStream(outputPath, FileMode.Create))
            {
                if (doc.ChildNodes.Count > 0 && doc.ChildNodes[0] is XmlProcessingInstruction)
                {
                    doc.RemoveChild(doc.ChildNodes[0]);
                }
                doc.Save(writer);
                writer.Flush();
                fileWriter.Write(memstream.GetBuffer(), 0, (Int32)memstream.Length);
            }
        }

        public static XmlDocument LoadFileXML(DataSetBase dataSetBase)
        {
            return Load(dataSetBase.TemplateFileNameAndPathAbsolute);
        }

    }
}
