using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using WeThePeople_ModdingTool.DataSets;
using WeThePeople_ModdingTool.Factories;
using WeThePeople_ModdingTool.FileUtilities;

namespace WeThePeople_ModdingTool.Creators
{
    public class EventCreatorRemoveEventTriggerInfoDone : EventCreatorBase
    {
        private TabItem tabItemToHide;
        public TabItem TabItem
        {
            set { tabItemToHide = value; }
        }

        private TextBox textBox_TriggerInfo_Done;
        public TextBox TextBox_TriggerInfo_Done
        {
            set { textBox_TriggerInfo_Done = value; }
        }
        public override bool Create()
        {
            if( false == Validate() )
            {
                return false;
            }
            tabItemToHide.Visibility = Visibility.Hidden;
            string eventInfoDoneToDelete = GetTextNameOfEventInfoDone(tabItemToHide.Name);
            DataSetXML dataSetXML = TemplateRepository.Instance.UnRegisterTemplateEventDone(eventInfoDoneToDelete);
            if (null == dataSetXML)
            {
                return false;
            }

            return RemoveEventTriggerInfoDone(XMLHelper.FindNodeByName(XMLHelper.GetFirstChildRootNodeList(dataSetXML), DataSetFactory.NODE_TYPE));
        }

        private bool RemoveEventTriggerInfoDone(XmlNode nodeNameToDelete)
        {
            DataSetXML eventTriggerInfo_Done = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventTriggerInfos_Done);
            XmlNodeList nodeEvents = eventTriggerInfo_Done.XmlDocumentProcessed.GetElementsByTagName(DataSetFactory.NODE_EVENTS);
            if (nodeEvents.Count != 1)
            {
                return false;
            }

            XmlNode nodeEvent = nodeEvents[0];

            foreach (XmlNode child in nodeEvent.ChildNodes)
            {
                if (child.InnerText.Equals(nodeNameToDelete.InnerText))
                {
                    nodeEvent.RemoveChild(child);
                    textBox_TriggerInfo_Done.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(eventTriggerInfo_Done));
                    return true;
                }
            }
            return false;
        }

        private string GetTextNameOfEventInfoDone(string tabItemName)
        {
            return tabItemName.Substring(8);
        }

        private bool Validate()
        {
            if (null == tabItemToHide)
            {
                return false;
            }

            if (null == textBox_TriggerInfo_Done)
            {
                return false;
            }

            return true;
        }

    }
}
