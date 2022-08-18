﻿using Serilog;
using System;
using System.IO;
using System.Text;
using System.Xml;
using WeThePeople_ModdingTool.DataSets;

namespace WeThePeople_ModdingTool.FileUtilities
{
    class XMLHelper
    {
        private static bool showMessageBox = false;

        public static XmlDocument CreateFromStringShowException(string xmlString)
        {
            showMessageBox = true;
            XmlDocument xmlDocument = CreateFromString(xmlString);
            showMessageBox = false;
            return xmlDocument;
        }
        public static XmlDocument CreateFromString( string xmlString )
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(xmlString);
                if (true == showMessageBox)
                {
                    CommonMessageBox.Show_Info("Information!", CommonVariables.VALIDATION_SUCCEEDED_CR +CommonVariables.XML_IS_SHAPELY);
                }
                return xmlDocument;
            }
            catch (Exception ex)
            {
                Log.Error(CommonVariables.MESSAGE_BOX_EXCEPTION + CommonVariables.COLON_BLANK + ex.Message);
                if( true == showMessageBox )
                {
                    CommonMessageBox.Show_OK_Error(CommonVariables.XML_ERROR, CommonVariables.MESSAGE_BOX_EXCEPTION_CR + CommonVariables.CR + ex.Message);
                }
                return null;
            }
        }
        public static bool IsXMLShapelyShowMessageBox(string xmlString)
        {
            showMessageBox = true;
            XmlDocument xmlDocument = CreateFromString(xmlString);
            showMessageBox = false;
            return xmlDocument != null;
        }

        public static bool IsXMLShapely( string xmlString )
        {
            XmlDocument xmlDocument = CreateFromString(xmlString);
            return xmlDocument != null;
        }

        public static string FormatKeepIndentionShowException(XmlNodeList nodes)
        {
            showMessageBox = true;
            string formated = FormatKeepIndention(nodes);
            showMessageBox = false;
            return formated;
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
                if (true == showMessageBox)
                {
                    CommonMessageBox.Show_OK_Error(CommonVariables.XML_ERROR, CommonVariables.MESSAGE_BOX_EXCEPTION +" Formatting failed!" + CommonVariables.CR + CommonVariables.CR + ex.Message);
                }
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
                    xmlTextWriter.Indentation = 4;
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlNode.WriteTo(xmlTextWriter);
                }
            }
            return stringBuilder.ToString();
        }

    }
}
