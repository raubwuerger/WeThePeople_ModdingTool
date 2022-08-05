using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WeThePeople_ModdingTool
{
    public class MainSettingsLoader
    {
        public XmlDocument CIV4EventInfos_template;
        public XmlDocument CIV4EventTriggerInfos_template;
        public XmlDocument CIV4GameText_Colonization_Events_utf8_template;
        public String CvRandomEventInterface_template;
        public bool LoadTemplates()
        {
            IO.XMLFileParser parser = new IO.XMLFileParser();
            String currentDir = Directory.GetCurrentDirectory();
            currentDir += "\\..\\..\\..\\templates\\Assets\\XML\\Events\\CIV4EventInfos_template.xml";
            parser.LoadFile(currentDir);

            return false;
        }
    }
}
