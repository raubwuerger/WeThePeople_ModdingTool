using ICSharpCode.AvalonEdit;
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

        private TextEditor textBox_TriggerInfo_Done;
        public TextEditor TextBox_TriggerInfo_Done
        {
            set { textBox_TriggerInfo_Done = value; }
        }

        private TextEditor textBox_EventGameText;
        public TextEditor TextBox_EventGameText
        {
            set { textBox_EventGameText = value; }
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

            if( false == RemoveEventTriggerInfoDone(XMLHelper.FindNodeByName(XMLHelper.GetFirstChildRootNodeList(dataSetXML), DataSetFactory.NODE_TYPE)) )
            {
                return false;
            }

            XmlNodeList description = dataSetXML.XmlDocumentProcessed.GetElementsByTagName(DataSetFactory.NODE_DESCRIPTION);
            if (description.Count != 1)
            {
                return false;
            }

            return RemoveGameTextDone(description[0].InnerText);
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

        private bool RemoveGameTextDone( string name )
        {
            if( null == TemplateRepository.Instance.UnRegisterTemplate(name) )
            {
                return false;
            }

            DataSetXML eventGameText = TemplateRepository.Instance.FindByNameXML(DataSetFactory.EventGameText);
            XmlNodeList nodeTags = eventGameText.XmlDocumentProcessed.GetElementsByTagName(DataSetFactory.NODE_TAG);
            if (nodeTags.Count <= 0)
            {
                return false;
            }

            for (int i = nodeTags.Count - 1; i >= 0; i--)
            {
                if (nodeTags[i].InnerText.Equals(name))
                {
                    XmlNode parentNode = nodeTags[i].ParentNode;
                    parentNode.ParentNode.RemoveChild(parentNode);
                    textBox_EventGameText.Text = XMLHelper.FormatKeepIndention(XMLHelper.GetRootNodeListProcessedXML(eventGameText));
                    return true;
                }
            }

            return true;
        }
    }
}
