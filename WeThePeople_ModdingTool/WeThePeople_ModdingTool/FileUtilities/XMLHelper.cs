using Serilog;
using System;
using System.Collections.Generic;
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
    }
}
