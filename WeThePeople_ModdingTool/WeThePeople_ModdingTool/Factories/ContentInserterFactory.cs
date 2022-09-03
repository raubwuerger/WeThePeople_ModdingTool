using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool.ContentInserter
{
    public class ContentInserterFactory
    {
        public static ContentInserterBase CreateContentInserterPython(string fileName)
        {
            ContentInserterBase contentInserterBase = new ContentInserterPython();
            contentInserterBase.FileName = fileName;
            return contentInserterBase;
        }

        public static ContentInserterBase CreateContentInserterByXmlDocument( string fileName, XmlDocument xmlDocument )
        {
            switch(xmlDocument.DocumentElement.Name)
            {
                case "sdf":
                    return CreateContentInserterGameText(fileName);
            }
            return null;
        }

        public static ContentInserterBase CreateContentInserterGameText(string fileName)
        {
            ContentInserterBase contentInserterBase = new ContentInserterXML();
            contentInserterBase.FileName = fileName;
            return contentInserterBase;
        }

        public static ContentInserterBase CreateContentInserterEventInfo(string fileName)
        {
            ContentInserterBase contentInserterBase = new ContentInserterXML();
            contentInserterBase.FileName = fileName;
            return contentInserterBase;
        }

        public static ContentInserterBase CreateContentInserterEventTriggerInfo(string fileName)
        {
            ContentInserterBase contentInserterBase = new ContentInserterXML();
            contentInserterBase.FileName = fileName;
            return contentInserterBase;
        }
    }
}
