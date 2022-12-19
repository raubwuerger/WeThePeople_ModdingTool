using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using WeThePeople_ModdingTool.DataSets;

namespace WeThePeople_ModdingTool.FileUtilities
{
    public class XMLFileUtility
    {
        static string FileName;

        public static XDocument Load(string fileName)
        {
            try
            {
                Log.Debug("Loading file: " + fileName);
                return XDocument.Load(fileName);
            }
            catch (Exception exception)
            {
                Log.Error("Exception occurred! " + exception.Message);
                return null;
            }
        }

        public static XDocument LoadWithReader(string fileName)
        {
            try
            {
                Log.Debug("Loading file: " + fileName);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var context = new XmlParserContext(null, null, null, XmlSpace.None);
                context.Encoding = Encoding.GetEncoding(1252);
                var reader = XmlReader.Create(fileName, null, context);
                reader.MoveToContent();
                return XDocument.Load(reader);
            }
            catch (Exception exeption)
            {
                Log.Error("Exception occurred: " + exeption.Message);
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

        public static XDocument LoadFileXML(DataSetBase dataSetBase)
        {
            return Load(dataSetBase.TemplateFileNameAndPathAbsolute);
        }

    }
}
