using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool.FileUtilities
{
    class XMLHelper
    {
        public static XmlDocument CreateFromString( string xmlString )
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(xmlString);
                return xmlDocument;
            }
            catch (Exception ex)
            {
                Log.Error(CommonVariables.MESSAGE_BOX_EXCEPTION + CommonVariables.COLON_BLANK + ex.Message);
                return null;
            }
        }

        public static bool IsXMLShapely( string xmlString )
        {
            XmlDocument xmlDocument = CreateFromString(xmlString);
            return xmlDocument != null;
        }

        public static string FormatKeepIndention( XmlNodeList nodes )
        {
            try
            { 
                string formatedXML = String.Empty;
                foreach (XmlNode node in nodes)
                {
                    formatedXML += FormatXml(node);
                }
                return formatedXML;
            }
            catch( Exception ex )
            {
                Log.Error(CommonVariables.MESSAGE_BOX_EXCEPTION + CommonVariables.COLON_BLANK + ex.Message);
                return String.Empty;
            }
        }

        public static string FormatXml(XmlNode xmlNode)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            {
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlNode.WriteTo(xmlTextWriter);
                }
            }
            return stringBuilder.ToString();
        }

    }
}
